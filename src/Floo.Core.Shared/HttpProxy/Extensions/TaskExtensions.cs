﻿using System;
using System.Threading.Tasks;

namespace Floo.Core.Shared.HttpProxy.Extensions
{
    internal static class TaskExtensions
    {
        public static readonly Task CompletedTask = Task.FromResult<object>(null);

        public static Task CastResult(this Task<object> taskResult, Type resultType)
        {
            var taskSource = new TaskCompletionSource(resultType);
            taskResult.ContinueWith((task) =>
            {
                try
                {
                    taskSource.SetResult(task.Result);
                }
                catch (AggregateException ex)
                {
                    taskSource.SetException(ex.InnerException);
                }
                catch (Exception ex)
                {
                    taskSource.SetException(ex);
                }
            });
            return taskSource.Task;
        }
    }
}