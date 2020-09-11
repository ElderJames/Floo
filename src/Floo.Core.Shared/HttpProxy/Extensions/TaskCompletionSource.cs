using System;
using System.Threading.Tasks;

namespace Floo.Core.Shared.HttpProxy.Extensions
{
    internal class TaskCompletionSource
    {
        private readonly ITaskCompletionSource taskSource;

        public Task Task
        {
            get
            {
                return this.taskSource.Task;
            }
        }

        public TaskCompletionSource(Type resultType)
        {
            var type = typeof(TaskCompletionSourceOf<>).MakeGenericType(resultType);
            this.taskSource = Activator.CreateInstance(type) as ITaskCompletionSource;
        }

        public bool SetResult(object result)
        {
            return this.taskSource.SetResult(result);
        }

        public bool SetException(Exception ex)
        {
            return this.taskSource.SetException(ex);
        }

        private interface ITaskCompletionSource
        {
            Task Task { get; }

            bool SetResult(object result);

            bool SetException(Exception ex);
        }

        private class TaskCompletionSourceOf<TResult> : ITaskCompletionSource
        {
            private readonly TaskCompletionSource<TResult> taskSource;

            public Task Task
            {
                get
                {
                    return this.taskSource.Task;
                }
            }

            public TaskCompletionSourceOf()
            {
                this.taskSource = new TaskCompletionSource<TResult>();
            }

            public bool SetResult(object result)
            {
                return this.taskSource.TrySetResult((TResult)result);
            }

            public bool SetException(Exception ex)
            {
                return this.taskSource.TrySetException(ex);
            }
        }
    }
}