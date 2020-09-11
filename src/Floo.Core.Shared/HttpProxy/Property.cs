using Floo.Core.Shared.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Floo.Core.Shared.HttpProxy
{
    internal class Property
    {
        private readonly Method getter;

        private readonly Method setter;

        public string Name { get; protected set; }

        public PropertyInfo Info { get; private set; }

        public bool IsVirtual { get; private set; }

        public Property(PropertyInfo property)
        {
            this.Name = property.Name;
            this.Info = property;

            var getMethod = property.GetGetMethod();
            if (getMethod != null)
            {
                this.getter = new Method(getMethod);
            }

            var setMethod = property.GetSetMethod();
            if (setMethod != null)
            {
                this.setter = new Method(setMethod);
            }
            this.IsVirtual = this.getter.Info.IsVirtual;
        }

        public object GetValue(object instance)
        {
            return this.getter.Invoke(instance, null);
        }

        public void SetValue(object instance, object value)
        {
            this.setter.Invoke(instance, value);
        }

        private static readonly ConcurrentCache<Type, Property[]> cached = new ConcurrentCache<Type, Property[]>();

        public static Property[] GetProperties(Type type)
        {
            return cached.GetOrAdd(type, t =>
              t.GetProperties().Select(p => new Property(p)).ToArray()
           );
        }

        private class Method
        {
            private readonly Func<object, object[], object> invoker;

            public string Name { get; protected set; }

            public MethodInfo Info { get; private set; }

            public Method(MethodInfo method)
            {
                this.Name = method.Name;
                this.Info = method;
                this.invoker = Method.CreateInvoker(method);
            }

            public object Invoke(object instance, params object[] parameters)
            {
                return this.invoker.Invoke(instance, parameters);
            }

            private static Func<object, object[], object> CreateInvoker(MethodInfo method)
            {
                var instance = Expression.Parameter(typeof(object), "instance");
                var parameters = Expression.Parameter(typeof(object[]), "parameters");

                var instanceCast = method.IsStatic ? null : Expression.Convert(instance, method.ReflectedType);
                var parametersCast = method.GetParameters().Select((p, i) =>
                {
                    var parameter = Expression.ArrayIndex(parameters, Expression.Constant(i));
                    return Expression.Convert(parameter, p.ParameterType);
                });

                var body = Expression.Call(instanceCast, method, parametersCast);

                if (method.ReturnType == typeof(void))
                {
                    var action = Expression.Lambda<Action<object, object[]>>(body, instance, parameters).Compile();
                    return (_instance, _parameters) =>
                    {
                        action.Invoke(_instance, _parameters);
                        return null;
                    };
                }
                else
                {
                    var bodyCast = Expression.Convert(body, typeof(object));
                    return Expression.Lambda<Func<object, object[], object>>(bodyCast, instance, parameters).Compile();
                }
            }
        }
    }
}