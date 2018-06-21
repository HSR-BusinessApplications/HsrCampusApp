// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace System.Threading.Tasks
{
    using Linq;

    public static class TaskExtensions
    {
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeSpan)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeSpan)))
            {
                return await task;
            }

            throw new TimeoutException();
        }

        public static void Finally(
            this Task task,
            Action finalAction,
            TaskScheduler scheduler = null)
        {
            if (finalAction == null)
            {
                throw new ArgumentNullException(nameof(finalAction), "finalAction cannot be null");
            }

            task.ContinueWith(t => finalAction(), scheduler ?? TaskScheduler.Default);
        }

        public static Task Catch<TException>(this Task task, Action<TException> exceptionHandler, TaskScheduler scheduler = null)
            where TException : Exception
        {
            if (exceptionHandler == null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler), "exceptionHandler cannot be null");
            }

            task.ContinueWith(
                t =>
                {
                    if (t.IsCanceled || !t.IsFaulted || t.Exception == null)
                    {
                        return;
                    }

                    var exception =
                        t.Exception.Flatten().InnerExceptions.FirstOrDefault() ?? t.Exception;

                    var exceptionToCatch = exception as TException;
                    if (exceptionToCatch != null)
                    {
                        exceptionHandler(exceptionToCatch);
                    }
                }, scheduler ?? TaskScheduler.Default);

            return task;
        }
    }
}
