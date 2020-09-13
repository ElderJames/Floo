using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Floo.Core.Shared.Utils
{
    public static class Mapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return MapperInternal<TSource, TTarget>.Map(source);
        }

        public static TTarget Map<TSource, TTarget>(TSource source, Action<TTarget> otherSetup)
            where TSource : class
            where TTarget : class
        {
            TTarget to = Map<TSource, TTarget>(source);
            otherSetup(to);
            return to;
        }

        public static IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> sourceList)
            where TSource : class
            where TTarget : class
        {
            return MapperInternal<TSource, TTarget>.MapList(sourceList);
        }

        public static IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> sourceList, Action<TSource, TTarget> otherSetup)
            where TSource : class
            where TTarget : class
        {
            return MapperInternal<TSource, TTarget>.MapList(sourceList, otherSetup);
        }

        public static List<TTarget> Map<TSource, TTarget>(List<TSource> sourceList)
            where TSource : class
            where TTarget : class
        {
            return MapperInternal<TSource, TTarget>.MapList(sourceList).ToList();
        }

        public static TTarget[] Map<TSource, TTarget>(TSource[] sourceList)
            where TSource : class
            where TTarget : class
        {
            return MapperInternal<TSource, TTarget>.MapList(sourceList).ToArray();
        }

        public static void Map<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            MapperInternal<TSource, TTarget>.Map(source, target);
        }

        private static class MapperInternal<TSource, TTarget>
            where TSource : class
            where TTarget : class
        {
            private static Func<TSource, TTarget> MapFunc { get; set; }

            private static Action<TSource, TTarget> MapAction { get; set; }

            public static TTarget Map(TSource source)
            {
                if (MapFunc == null)
                {
                    MapFunc = GetMapFunc();
                }

                return MapFunc(source);
            }

            public static IEnumerable<TTarget> MapList(IEnumerable<TSource> sources)
            {
                if (MapFunc == null)
                {
                    MapFunc = GetMapFunc();
                }

                return sources.Select(MapFunc);
            }

            public static IEnumerable<TTarget> MapList(IEnumerable<TSource> sources, Action<TSource, TTarget> otherSetup)
            {
                if (MapFunc == null)
                {
                    MapFunc = GetMapFunc();
                }

                return sources.Select((s) =>
                {
                    var target = MapFunc(s);
                    otherSetup(s, target);
                    return target;
                });
            }

            public static void Map(TSource source, TTarget target)
            {
                if (MapAction == null)
                {
                    MapAction = GetMapAction();
                }

                MapAction(source, target);
            }

            private static Func<TSource, TTarget> GetMapFunc()
            {
                var sourceType = typeof(TSource);
                var targetType = typeof(TTarget);

                if (IsEnumerable(sourceType) || IsEnumerable(targetType))
                {
                    throw new NotSupportedException("Enumerable types are not supported,please use MapList method.");
                }

                var parameter = Expression.Parameter(sourceType, "p");

                var memberBindings = new List<MemberBinding>();
                var targetTypes = targetType.GetProperties().Where(x => x.CanWrite);
                foreach (var targetItem in targetTypes)
                {
                    var sourceItem = sourceType.GetProperty(targetItem.Name);

                    if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic)
                    {
                        continue;
                    }

                    if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null)
                    {
                        continue;
                    }

                    var sourceProperty = Expression.Property(parameter, sourceItem);

                    try
                    {
                        if (!sourceItem.PropertyType.IsValueType && sourceItem.PropertyType != targetItem.PropertyType)
                        {
                            if (sourceItem.PropertyType.IsClass && targetItem.PropertyType.IsClass
                                && !sourceItem.PropertyType.IsArray && !targetItem.PropertyType.IsArray
                                && !sourceItem.PropertyType.IsGenericType && !targetItem.PropertyType.IsGenericType)
                            {
                                var expression = GetClassExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                                memberBindings.Add(Expression.Bind(targetItem, expression));
                            }

                            if (typeof(IEnumerable).IsAssignableFrom(sourceItem.PropertyType) && typeof(IEnumerable).IsAssignableFrom(targetItem.PropertyType))
                            {
                                var expression = GetListExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                                memberBindings.Add(Expression.Bind(targetItem, expression));
                            }

                            continue;
                        }

                        if (IsNullableType(sourceItem.PropertyType) && !IsNullableType(targetItem.PropertyType))
                        {
                            var hasValueExpression = Expression.Equal(Expression.Property(sourceProperty, "HasValue"), Expression.Constant(true));

                            var conditionItem = Expression.Condition(hasValueExpression, Expression.Convert(sourceProperty, targetItem.PropertyType), Expression.Default(targetItem.PropertyType));
                            memberBindings.Add(Expression.Bind(targetItem, conditionItem));

                            continue;
                        }

                        if (!IsNullableType(sourceItem.PropertyType) && IsNullableType(targetItem.PropertyType))
                        {
                            var memberExpression = Expression.Convert(sourceProperty, targetItem.PropertyType);
                            memberBindings.Add(Expression.Bind(targetItem, memberExpression));
                            continue;
                        }

                        if (targetItem.PropertyType != sourceItem.PropertyType)
                        {
                            continue;
                        }

                        memberBindings.Add(Expression.Bind(targetItem, sourceProperty));
                    }
                    catch (Exception ex)
                    {
                        memberBindings.Add(Expression.Bind(targetItem, Expression.Default(targetItem.PropertyType)));
                        Debug.Fail(ex.Message, ex.StackTrace);
                    }
                }

                var test = Expression.NotEqual(parameter, Expression.Constant(null, sourceType)); // p==null;
                var ifTrue = Expression.MemberInit(Expression.New(targetType), memberBindings);
                var condition = Expression.Condition(test, ifTrue, Expression.Constant(null, targetType));

                var lambda = Expression.Lambda<Func<TSource, TTarget>>(condition, parameter);
                return lambda.Compile();
            }

            private static Expression GetClassExpression(Expression sourceProperty, Type sourceType, Type targetType)
            {
                var testItem = Expression.NotEqual(sourceProperty, Expression.Constant(null, sourceType));

                var mapperType = typeof(MapperInternal<,>).MakeGenericType(sourceType, targetType);
                var ifTrue = Expression.Call(mapperType.GetMethod(nameof(Map), new[] { sourceType }) ?? throw new InvalidOperationException(), sourceProperty);

                var conditionItem = Expression.Condition(testItem, ifTrue, Expression.Constant(null, targetType));

                return conditionItem;
            }

            private static Expression GetListExpression(Expression sourceProperty, Type sourceType, Type targetType)
            {
                var testItem = Expression.NotEqual(sourceProperty, Expression.Constant(null, sourceType));

                var sourceArg = sourceType.IsArray ? sourceType.GetElementType() : sourceType.GetGenericArguments()[0];
                var targetArg = targetType.IsArray ? targetType.GetElementType() : targetType.GetGenericArguments()[0];
                var mapperType = typeof(MapperInternal<,>).MakeGenericType(sourceArg, targetArg);

                var mapperExecMap = Expression.Call(mapperType.GetMethod(nameof(MapList), new[] { sourceType }), sourceProperty);

                Expression ifTrue;
                if (targetType == mapperExecMap.Type)
                {
                    ifTrue = mapperExecMap;
                }
                else if (targetType.IsArray)
                {
                    ifTrue = Expression.Call(typeof(Enumerable), nameof(Enumerable.ToArray), new[] { mapperExecMap.Type.GenericTypeArguments[0] }, mapperExecMap);
                }
                else if (typeof(IDictionary).IsAssignableFrom(targetType))
                {
                    ifTrue = Expression.Constant(null, targetType);
                }
                else
                {
                    ifTrue = Expression.Convert(mapperExecMap, targetType);
                }

                var conditionItem = Expression.Condition(testItem, ifTrue, Expression.Constant(null, targetType));

                return conditionItem;
            }

            private static Action<TSource, TTarget> GetMapAction()
            {
                var sourceType = typeof(TSource);
                var targetType = typeof(TTarget);

                if (IsEnumerable(sourceType) || IsEnumerable(targetType))
                {
                    throw new NotSupportedException("Enumerable types are not supported,please use MapList method.");
                }

                var sourceParameter = Expression.Parameter(sourceType, "p");
                var targetParameter = Expression.Parameter(targetType, "t");

                var expressions = new List<Expression>();

                var targetTypes = targetType.GetProperties().Where(x => x.PropertyType.IsPublic && x.CanWrite);
                foreach (var targetItem in targetTypes)
                {
                    var sourceItem = sourceType.GetProperty(targetItem.Name);

                    if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic)
                    {
                        continue;
                    }

                    if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null)
                    {
                        continue;
                    }

                    var sourceProperty = Expression.Property(sourceParameter, sourceItem);
                    var targetProperty = Expression.Property(targetParameter, targetItem);

                    try
                    {
                        if (!sourceItem.PropertyType.IsValueType && sourceItem.PropertyType != targetItem.PropertyType)
                        {
                            if (sourceItem.PropertyType.IsClass && targetItem.PropertyType.IsClass
                                && !sourceItem.PropertyType.IsArray && !targetItem.PropertyType.IsArray
                                && !sourceItem.PropertyType.IsGenericType && !targetItem.PropertyType.IsGenericType)
                            {
                                var expression = GetClassExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                                expressions.Add(Expression.Assign(targetProperty, expression));
                            }

                            if (typeof(IEnumerable).IsAssignableFrom(sourceItem.PropertyType) && typeof(IEnumerable).IsAssignableFrom(targetItem.PropertyType))
                            {
                                var expression = GetListExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                                expressions.Add(Expression.Assign(targetProperty, expression));
                            }

                            continue;
                        }

                        if (IsNullableType(sourceItem.PropertyType) && !IsNullableType(targetItem.PropertyType))
                        {
                            var hasValueExpression = Expression.Equal(Expression.Property(sourceProperty, "HasValue"), Expression.Constant(true));
                            var notEqualValueExpression = Expression.NotEqual(Expression.Property(sourceProperty, "Value"), targetProperty);
                            var notEqualCondition = Expression.IfThen(notEqualValueExpression, Expression.Assign(targetProperty, Expression.Convert(sourceProperty, targetItem.PropertyType)));

                            expressions.Add(Expression.IfThenElse(hasValueExpression, notEqualCondition, Expression.Assign(targetProperty, Expression.Default(targetItem.PropertyType))));
                            continue;
                        }

                        if (!IsNullableType(sourceItem.PropertyType) && IsNullableType(targetItem.PropertyType))
                        {
                            var hasValueExpression = Expression.Equal(Expression.Property(targetProperty, "HasValue"), Expression.Constant(true));
                            var notEqualValueExpression = Expression.And(hasValueExpression, Expression.NotEqual(sourceProperty, Expression.Property(targetProperty, "Value")));
                            var memberExpression = Expression.Convert(sourceProperty, targetItem.PropertyType);
                            expressions.Add(Expression.IfThen(notEqualValueExpression, Expression.Assign(targetProperty, memberExpression)));
                            continue;
                        }

                        if (targetItem.PropertyType != sourceItem.PropertyType)
                        {
                            continue;
                        }

                        var notEqualExpression = Expression.NotEqual(sourceProperty, targetProperty);
                        var conditionExpression = Expression.IfThen(notEqualExpression, Expression.Assign(targetProperty, sourceProperty));
                        expressions.Add(conditionExpression);
                    }
                    catch (Exception ex)
                    {
                        expressions.Add(Expression.Assign(targetProperty, Expression.Default(targetItem.PropertyType)));
                        Debug.Fail(ex.Message, ex.StackTrace);
                    }
                }

                var testSource = Expression.NotEqual(sourceParameter, Expression.Constant(null, sourceType));
                var ifTrueSource = Expression.Block(expressions);
                var conditionSource = Expression.IfThen(testSource, ifTrueSource);

                var testTarget = Expression.NotEqual(targetParameter, Expression.Constant(null, targetType));
                var conditionTarget = Expression.IfThen(testTarget, conditionSource);

                var lambda = Expression.Lambda<Action<TSource, TTarget>>(conditionTarget, sourceParameter, targetParameter);
                return lambda.Compile();
            }

            private static bool IsNullableType(Type type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            private static bool IsEnumerable(Type type)
            {
                return type.IsArray || type.GetInterfaces().Any(x => x == typeof(ICollection) || x == typeof(IEnumerable));
            }
        }
    }
}