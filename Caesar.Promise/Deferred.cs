using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesar.Promise
{
    /// <summary>
    /// A generic object deferred
    /// </summary>
    public class Deferred : Deferred<object, object>
    {
    }

    public class Deferred<T, TFail> : Promise<T, TFail>
    {
        /// <summary>
        /// A list of registered calbacks.
        /// </summary>
        private List<Callback> callbacks = new List<Callback>();

        /// <summary>
        /// States if the promise is resolved.
        /// </summary>
        protected bool _isResolved = false;

        /// <summary>
        /// States if the promise is rejected.
        /// </summary>
        protected bool _isRejected = false;

        /// <summary>
        /// Contains the generic argument by which the promise is fulfilled.
        /// </summary>
        private dynamic _arg;

        /// <summary>
        /// Combines an IEnumerable of promises into one promise.
        /// </summary>
        /// <param name="promises">An IEnumerable of to be fulfilled promises.</param>
        /// <returns>A promise that is based on the given IEnumerable of promises.</returns>
        public static Promise When(IEnumerable<Promise> promises)
        {
            var count = 0;
            var masterPromise = new Deferred();

            foreach (var p in promises)
            {
                count++;
                p.Fail(() =>
                {
                    masterPromise.Reject();
                });
                p.Done(() =>
                {
                    count--;
                    if (0 == count)
                    {
                        masterPromise.Resolve();
                    }
                });
            }

            return masterPromise;
        }

        /// <summary>
        /// Creates a promise of a deferred.
        /// </summary>
        /// <returns>The created promise.</returns>
        public Promise<T, TFail> Promise()
        {
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        public Promise Always(Action callback)
        {
            if (_isResolved || _isRejected)
                callback();
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Always, false));
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Always(Action<dynamic> callback)
        {
            if (_isResolved || _isRejected)
                callback(_arg);
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Always, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Always(IEnumerable<Action<dynamic>> callbacks)
        {
            foreach (Action<dynamic> callback in callbacks)
                Always(callback);
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public Promise Done(Action callback)
        {
            if (_isResolved)
                callback();
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Success, false));
            return this;
        }

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Done(Action<T> callback)
        {
            if (_isResolved)
                callback(_arg);
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Success, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Done(IEnumerable<Action<T>> callbacks)
        {
            foreach (Action<T> callback in callbacks)
                Done(callback);
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        public Promise Fail(Action callback)
        {
            if (_isRejected)
                callback();
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Fail, false));
            return this;
        }

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Fail(Action<TFail> callback)
        {
            if (_isRejected)
                callback(_arg);
            else
                callbacks.Add(new Callback(callback, Callback.Condition.Fail, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic list of callbacks of type action</param>
        /// <returns>Itself</returns>
        public Promise<T, TFail> Fail(IEnumerable<Action<TFail>> callbacks)
        {
            foreach (Action<TFail> callback in callbacks)
                Fail(callback);
            return this;
        }

        /// <summary>
        /// States if the promise is rejected.
        /// </summary>
        public bool IsRejected
        {
            get { return _isRejected; }
        }

        /// <summary>
        /// States if the promise is resolved.
        /// </summary>
        public bool IsResolved
        {
            get { return _isResolved; }
        }

        /// <summary>
        /// States if the promise is fulfilled.
        /// </summary>
        public bool IsFulfilled
        {
            get { return _isRejected || _isResolved; }
        }

        /// <summary>
        /// Rejects the deferred and therefore invokes all registered fail callbacks.
        /// </summary>
        /// <returns>Itself</returns>
        public Promise Reject()
        {
            if (_isRejected || _isResolved) // ignore if already rejected or resolved
                return this;
            _isRejected = true;
            DequeueCallbacks(Callback.Condition.Fail);
            return this;
        }

        /// <summary>
        /// Rejects the deferred and therefore invokes all registered fail callbacks.
        /// </summary>
        /// <param name="arg">A generic argument which be passed through to all callbacks.</param>
        /// <returns>Itself</returns>
        public Deferred<T, TFail> Reject(TFail arg)
        {
            if (_isRejected || _isResolved) // ignore if already rejected or resolved
                return this;
            _isRejected = true;
            _arg = arg;
            DequeueCallbacks(Callback.Condition.Fail);
            return this;
        }

        /// <summary>
        /// Resolves the deferred and therefore invokes all registered done callbacks.
        /// </summary>
        /// <returns>Itself</returns>
        public Promise Resolve()
        {
            if (_isRejected || _isResolved) // ignore if already rejected or resolved
                return this;
            _isResolved = true;
            DequeueCallbacks(Callback.Condition.Success);
            return this;
        }

        /// <summary>
        /// Resolves the deferred and therefore invokes all registered done callbacks.
        /// </summary>
        /// <param name="arg">A generic argument which be passed through to all callbacks.</param>
        /// <returns>Itself</returns>
        public Deferred<T, TFail> Resolve(T arg)
        {
            if (_isRejected || _isResolved) // ignore if already rejected or resolved
                return this;
            _isResolved = true;
            _arg = arg;
            DequeueCallbacks(Callback.Condition.Success);
            return this;
        }

        /// <summary>
        /// Invokes all registered callbacks if the registration matches the rejection of resolution.
        /// </summary>
        /// <param name="cond"></param>
        private void DequeueCallbacks(Callback.Condition cond)
        {
            foreach (Callback callback in callbacks)
            {
                if (callback.Cond == cond || callback.Cond == Callback.Condition.Always)
                {
                    if (callback.IsReturnValue)
                        callback.Del.DynamicInvoke(_arg);
                    else
                        callback.Del.DynamicInvoke();
                }
            }
            callbacks.Clear();
        }
    }
}
