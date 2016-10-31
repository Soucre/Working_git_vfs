using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NCommon
{
     /// <summary>Any long running task should be run on a background thread.
    /// This reduces the chance of your web server becoming unresponsive.
    /// The AsyncTask uses SimpleInjector to create an instance of the requested type T. 
    /// </summary>
    /// <example>
    /// For this example, imagine we want to do heavy image processing in response to a file being uploaded. 
    /// Our action method will be:
    /// <code lang="cs"><![CDATA[
    /// public ActionResult UploadImage(HttpPostedFileBase image) {
    ///    image.SaveAs("example.jpg");
    ///    AsyncTask.Render<ImageResizer>(resizer => resizer.Resize("example.jpg"));
    ///    return RedirectToAction("UploadFinished");
    /// }
    /// ]]></code>
    /// <see cref="Container"/> instance as default dependency resolver in MVC.
    /// </example>
    public static class AsyncTask
    {
        // Fields
        private static readonly Task<object> _completedTaskReturningNull = FromResult<object>(null);
        private static readonly Task<AsyncVoid> _defaultVoidCompleted = FromResult<AsyncVoid>(new AsyncVoid());
        private static readonly Task _defaultCompleted = FromResult<AsyncVoid>(new AsyncVoid());

        #region Render

        public static Task Run(this Action action)
        {
            return Task.Factory.StartNew(() => action());
        }

        public static Task SafeRun(this Action action)
        {
            return Task.Factory.StartNew(() => {
                try {
                    action();
                }
                catch { }
            });
        }

        public static Task Run(this Action action, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => action(), cancellationToken);
        }

        public static Task SafeRun(this Action action, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                try {
                    action();
                }
                catch { }
            }, cancellationToken);
        }

        public static Task<TResult> Run<TResult>(this Func<TResult> func)
        {
            return Task.Factory.StartNew(() => func());
        }

        public static Task<TResult> Run<TResult>(this Func<TResult> func, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => func(), cancellationToken);
        }

        public static Task Run<TResult>(this Func<TResult> func, Action<TResult> callback)
        {
            return Task.Factory.StartNew(() => func()).ContinueWith(t => callback(t.Result));
        }

        public static Task Run<TResult>(this Func<TResult> func, Action<TResult> callback, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => func(), cancellationToken).ContinueWith(t => callback(t.Result));
        }

        #endregion

        #region FromResult and Errors

        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
            source.SetResult(result);
            return source.Task;
        }

        public static Task FromError(Exception exception)
        {
            return FromError<AsyncVoid>(exception);
        }

        public static Task<TResult> FromError<TResult>(Exception exception)
        {
            TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
            source.SetException(exception);
            return source.Task;
        }

        public static Task FromErrors(IEnumerable<Exception> exceptions)
        {
            return FromErrors<AsyncVoid>(exceptions);
        }

        public static Task<TResult> FromErrors<TResult>(IEnumerable<Exception> exceptions)
        {
            TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
            source.SetException(exceptions);
            return source.Task;
        }

        public static Task<object> NullResult()
        {
            return _completedTaskReturningNull;
        }

        #endregion

        #region Complete and Canceled

        public static Task Completed()
        {
            return _defaultCompleted;
        }

        public static Task Canceled()
        {
            return CancelCache<AsyncVoid>.Canceled;
        }

        public static Task<TResult> Canceled<TResult>()
        {
            return CancelCache<TResult>.Canceled;
        }

        #endregion

        #region Await

        public static void Await(this Task task)
        {
            task.Wait();

            if (task.IsFaulted || task.IsCanceled) { // in case of failure    
                throw task.Exception ?? new Exception();
            }
        }

        public static T Await<T>(this Task<T> task)
        {
            task.Wait();

            if (task.IsFaulted || task.IsCanceled) { // in case of failure    
                throw task.Exception ?? new Exception();
            }

            return task.Result;
        }

        #endregion

        #region RunSynchronously

        public static Task RunSynchronously(Action action, CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested) {
                return Canceled();
            }
            try {
                action();
                return Completed();
            }
            catch (Exception exception) {
                return FromError(exception);
            }
        }

        public static Task<TResult> RunSynchronously<TResult>(Func<TResult> func, CancellationToken cancellationToken = new CancellationToken())
        {
            if (cancellationToken.IsCancellationRequested) {
                return Canceled<TResult>();
            }
            try {
                return FromResult<TResult>(func());
            }
            catch (Exception exception) {
                return FromError<TResult>(exception);
            }
        }

        public static Task<TResult> RunSynchronously<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = new CancellationToken())
        {
            if (cancellationToken.IsCancellationRequested) {
                return Canceled<TResult>();
            }
            try {
                return func();
            }
            catch (Exception exception) {
                return FromError<TResult>(exception);
            }
        }

        #endregion

        #region Cast

        public static Task<TOuterResult> CastFromObject<TOuterResult>(this Task<object> task)
        {
            if (task.IsCompleted) {
                if (task.IsFaulted) {
                    return FromErrors<TOuterResult>(task.Exception.InnerExceptions);
                }
                if (task.IsCanceled) {
                    return Canceled<TOuterResult>();
                }
                if (task.Status == TaskStatus.RanToCompletion) {
                    try {
                        return FromResult<TOuterResult>((TOuterResult)task.Result);
                    }
                    catch (Exception exception) {
                        return FromError<TOuterResult>(exception);
                    }
                }
            }

            TaskCompletionSource<TOuterResult> tcs = new TaskCompletionSource<TOuterResult>();
            task.ContinueWith(delegate(Task<object> innerTask) {
                if (innerTask.IsFaulted) {
                    tcs.SetException(innerTask.Exception.InnerExceptions);
                }
                else if (innerTask.IsCanceled) {
                    tcs.SetCanceled();
                }
                else {
                    try {
                        tcs.SetResult((TOuterResult)innerTask.Result);
                    }
                    catch (Exception exception) {
                        tcs.SetException(exception);
                    }
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        public static Task<object> CastToObject<T>(this Task<T> task)
        {
            if (task.IsCompleted) {
                if (task.IsFaulted) {
                    return FromErrors<object>(task.Exception.InnerExceptions);
                }
                if (task.IsCanceled) {
                    return Canceled<object>();
                }
                if (task.Status == TaskStatus.RanToCompletion) {
                    return FromResult<object>(task.Result);
                }
            }

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate(Task<T> innerTask) {
                if (innerTask.IsFaulted) {
                    tcs.SetException(innerTask.Exception.InnerExceptions);
                }
                else if (innerTask.IsCanceled) {
                    tcs.SetCanceled();
                }
                else {
                    tcs.SetResult(innerTask.Result);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        public static Task<object> CastToObject(this Task task)
        {
            if (task.IsCompleted) {
                if (task.IsFaulted) {
                    return FromErrors<object>(task.Exception.InnerExceptions);
                }
                if (task.IsCanceled) {
                    return Canceled<object>();
                }
                if (task.Status == TaskStatus.RanToCompletion) {
                    return FromResult<object>(null);
                }
            }

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate(Task innerTask) {
                if (innerTask.IsFaulted) {
                    tcs.SetException(innerTask.Exception.InnerExceptions);
                }
                else if (innerTask.IsCanceled) {
                    tcs.SetCanceled();
                }
                else {
                    tcs.SetResult(null);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        #endregion

        #region Then

        public static Task Then(this Task task, Action continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task, AsyncVoid>(t => ToAsyncVoidTask(continuation), cancellationToken, runSynchronously);
        }

        public static Task<TOuterResult> Then<TOuterResult>(this Task task, Func<TOuterResult> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task, TOuterResult>(t => AsyncTask.FromResult<TOuterResult>(continuation()), cancellationToken, runSynchronously);
        }

        public static Task Then(this Task task, Func<Task> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.Then<AsyncVoid>(((Func<Task<AsyncVoid>>)(() => continuation().Then<AsyncVoid>(((Func<AsyncVoid>)(() => new AsyncVoid())), new CancellationToken(), false))), cancellationToken, runSynchronously);
        }

        public static Task<TOuterResult> Then<TOuterResult>(this Task task, Func<Task<TOuterResult>> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task, TOuterResult>(t => continuation(), cancellationToken, runSynchronously);
        }

        public static Task Then<TInnerResult>(this Task<TInnerResult> task, Action<TInnerResult> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task<TInnerResult>, AsyncVoid>(t => ToAsyncVoidTask(delegate {
                continuation(t.Result);
            }), cancellationToken, runSynchronously);
        }

        public static Task<TOuterResult> Then<TInnerResult, TOuterResult>(this Task<TInnerResult> task, Func<TInnerResult, TOuterResult> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task<TInnerResult>, TOuterResult>(t => AsyncTask.FromResult<TOuterResult>(continuation(t.Result)), cancellationToken, runSynchronously);
        }

        public static Task Then<TInnerResult>(this Task<TInnerResult> task, Func<TInnerResult, Task> continuation, CancellationToken token = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task<TInnerResult>, AsyncVoid>(t => continuation(t.Result).ToTask<AsyncVoid>(new CancellationToken(), new AsyncVoid()), token, runSynchronously);
        }

        public static Task<TOuterResult> Then<TInnerResult, TOuterResult>(this Task<TInnerResult> task, Func<TInnerResult, Task<TOuterResult>> continuation, CancellationToken cancellationToken = new CancellationToken(), bool runSynchronously = false)
        {
            return task.ThenImpl<Task<TInnerResult>, TOuterResult>(t => continuation(t.Result), cancellationToken, runSynchronously);
        }

        private static Task<TOuterResult> ThenImpl<TTask, TOuterResult>(this TTask task, Func<TTask, Task<TOuterResult>> continuation, CancellationToken cancellationToken, bool runSynchronously) where TTask : Task
        {
            if (task.IsCompleted) {
                if (task.IsFaulted) {
                    return AsyncTask.FromErrors<TOuterResult>(task.Exception.InnerExceptions);
                }
                if (task.IsCanceled || cancellationToken.IsCancellationRequested) {
                    return AsyncTask.Canceled<TOuterResult>();
                }
                if (task.Status == TaskStatus.RanToCompletion) {
                    try {
                        return continuation(task);
                    }
                    catch (Exception exception) {
                        return AsyncTask.FromError<TOuterResult>(exception);
                    }
                }
            }
            return ThenImplContinuation<TOuterResult, TTask>(task, continuation, cancellationToken, runSynchronously);
        }

        private static Task<TOuterResult> ThenImplContinuation<TOuterResult, TTask>(TTask task, Func<TTask, Task<TOuterResult>> continuation, CancellationToken cancellationToken, bool runSynchronously = false) where TTask : Task
        {
            SynchronizationContext syncContext = SynchronizationContext.Current;
            TaskCompletionSource<Task<TOuterResult>> tcs = new TaskCompletionSource<Task<TOuterResult>>();
            task.ContinueWith(delegate(Task innerTask) {
                if (innerTask.IsFaulted) {
                    tcs.TrySetException(innerTask.Exception.InnerExceptions);
                }
                else if (innerTask.IsCanceled || cancellationToken.IsCancellationRequested) {
                    tcs.TrySetCanceled();
                }
                else if (syncContext != null) {
                    SendOrPostCallback d = (x) => {
                        try {
                            tcs.TrySetResult(continuation(task));
                        }
                        catch (Exception exception) {
                            tcs.TrySetException(exception);
                        }
                    };

                    syncContext.Post(d, null);
                }
                else {
                    tcs.TrySetResult(continuation(task));
                }
            }, runSynchronously ? TaskContinuationOptions.ExecuteSynchronously : TaskContinuationOptions.None);

            return tcs.Task.FastUnwrap<TOuterResult>();
        }

        #endregion

        #region Misc Helpers

        public static Task FastUnwrap(this Task<Task> task)
        {
            Task task2 = (task.Status == TaskStatus.RanToCompletion) ? task.Result : null;
            return (task2 ?? task.Unwrap());
        }

        public static Task<TResult> FastUnwrap<TResult>(this Task<Task<TResult>> task)
        {
            Task<TResult> task2 = (task.Status == TaskStatus.RanToCompletion) ? task.Result : null;
            return (task2 ?? task.Unwrap<TResult>());
        }


        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<TResult> tcs, Task source)
        {
            if (source.Status == TaskStatus.Canceled) {
                return tcs.TrySetCanceled();
            }
            if (source.Status == TaskStatus.Faulted) {
                return tcs.TrySetException(source.Exception.InnerExceptions);
            }
            if (source.Status == TaskStatus.RanToCompletion) {
                Task<TResult> task = source as Task<TResult>;
                return tcs.TrySetResult((task == null) ? default(TResult) : task.Result);
            }
            return false;
        }

        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<Task<TResult>> tcs, Task source)
        {
            if (source.Status == TaskStatus.Canceled) {
                return tcs.TrySetCanceled();
            }
            if (source.Status == TaskStatus.Faulted) {
                return tcs.TrySetException(source.Exception.InnerExceptions);
            }
            if (source.Status != TaskStatus.RanToCompletion) {
                return false;
            }
            Task<Task<TResult>> task = source as Task<Task<TResult>>;
            if (task != null) {
                return tcs.TrySetResult(task.Result);
            }
            Task<TResult> result = source as Task<TResult>;
            if (result != null) {
                return tcs.TrySetResult(result);
            }
            return tcs.TrySetResult(FromResult<TResult>(default(TResult)));
        }

        public static Task<AsyncVoid> ToAsyncVoidTask(this Action action)
        {
            return AsyncTask.RunSynchronously<AsyncVoid>(() => {
                action();
                return _defaultVoidCompleted;
            }, new CancellationToken());
        }

        public static Task<TResult> ToTask<TResult>(this Task task, CancellationToken cancellationToken = new CancellationToken(), TResult result = default(TResult))
        {
            if (task == null) {
                return null;
            }
            if (task.IsCompleted) {
                if (task.IsFaulted) {
                    return AsyncTask.FromErrors<TResult>(task.Exception.InnerExceptions);
                }
                if (task.IsCanceled || cancellationToken.IsCancellationRequested) {
                    return AsyncTask.Canceled<TResult>();
                }
                if (task.Status == TaskStatus.RanToCompletion) {
                    return AsyncTask.FromResult<TResult>(result);
                }
            }
            return ToTaskContinuation<TResult>(task, result);
        }

        private static Task<TResult> ToTaskContinuation<TResult>(Task task, TResult result)
        {
            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
            task.ContinueWith(delegate(Task innerTask) {
                if (task.Status == TaskStatus.RanToCompletion) {
                    tcs.TrySetResult(result);
                }
                else {
                    tcs.TrySetFromTask<TResult>(innerTask);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        #endregion

        // Nested Types
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct AsyncVoid
        {
        }

        private static class CancelCache<TResult>
        {
            // Fields
            public static readonly Task<TResult> Canceled;

            // Methods
            static CancelCache()
            {
                AsyncTask.CancelCache<TResult>.Canceled = AsyncTask.CancelCache<TResult>.GetCancelledTask();
            }

            private static Task<TResult> GetCancelledTask()
            {
                TaskCompletionSource<TResult> source = new TaskCompletionSource<TResult>();
                source.SetCanceled();
                return source.Task;
            }
        }
    }
}
