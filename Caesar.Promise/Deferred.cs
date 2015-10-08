using System;
using System.Collections.Generic;

namespace Caesar.Promise
{
    /// <summary>
    /// A generic object deferred
    /// </summary>
    public class Deferred : Deferred<object, object>
    {
    }

    /// <summary>
    /// A chainable utility object that can be used to register and invoke multiple callbacks to influent the flow of possibly asynchronous code.
    /// </summary>
    /// <typeparam name="T">The type of Resolve argument that gets passed into the Done callbacks.</typeparam>
    /// <typeparam name="TFail">The type of the Reject argument that gets passed into the Fail callbacks.</typeparam>
    public class Deferred<T, TFail> : IPromise<T, TFail>
    {
        /// <summary>
        /// A list of registered calbacks.
        /// </summary>
        private readonly List<Callback> _callbacks = new List<Callback>();

        /// <summary>
        /// Contains the generic argument by which the promise is fulfilled.
        /// </summary>
        private dynamic _arg;

        /// <summary>
        /// Combines an IEnumerable of promises into one promise.
        /// </summary>
        /// <param name="promises">An IEnumerable of to be fulfilled promises.</param>
        /// <returns>A promise that is based on the given IEnumerable of promises.</returns>
        public static IPromise When(IEnumerable<IPromise> promises)
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
                    // ReSharper disable once AccessToModifiedClosure
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
        public IPromise<T, TFail> Promise()
        {
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise Always(Action callback)
        {
            if (IsResolved || IsRejected)
                callback();
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Always, false));
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Always(Action<dynamic> callback)
        {
            if (IsResolved || IsRejected)
                callback(_arg);
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Always, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Always(IEnumerable<Action<dynamic>> callbacks)
        {
            foreach (var callback in callbacks)
                Always(callback);
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise Done(Action callback)
        {
            if (IsResolved)
                callback();
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Success, false));
            return this;
        }

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Done(Action<T> callback)
        {
            if (IsResolved)
                callback(_arg);
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Success, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Done(IEnumerable<Action<T>> callbacks)
        {
            foreach (var callback in callbacks)
                Done(callback);
            return this;
        }

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise Fail(Action callback)
        {
            if (IsRejected)
                callback();
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Fail, false));
            return this;
        }

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Fail(Action<TFail> callback)
        {
            if (IsRejected)
                callback(_arg);
            else
                _callbacks.Add(new Callback(callback, Callback.Condition.Fail, true));
            return this;
        }

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callbacks">A generic list of callbacks of type action</param>
        /// <returns>Itself</returns>
        public IPromise<T, TFail> Fail(IEnumerable<Action<TFail>> callbacks)
        {
            foreach (var callback in callbacks)
                Fail(callback);
            return this;
        }

        /// <summary>
        /// States if the promise is rejected.
        /// </summary>
        public bool IsRejected { get; protected set; }

        /// <summary>
        /// States if the promise is resolved.
        /// </summary>
        public bool IsResolved { get; protected set; }

        /// <summary>
        /// States if the promise is fulfilled.
        /// </summary>
        public bool IsFulfilled => IsRejected || IsResolved;

        /// <summary>
        /// Rejects the deferred and therefore invokes all registered fail callbacks.
        /// </summary>
        /// <returns>Itself</returns>
        public IPromise Reject()
        {
            if (IsRejected || IsResolved) // ignore if already rejected or resolved
                return this;
            IsRejected = true;
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
            if (IsRejected || IsResolved) // ignore if already rejected or resolved
                return this;
            IsRejected = true;
            _arg = arg;
            DequeueCallbacks(Callback.Condition.Fail);
            return this;
        }

        /// <summary>
        /// Resolves the deferred and therefore invokes all registered done callbacks.
        /// </summary>
        /// <returns>Itself</returns>
        public IPromise Resolve()
        {
            if (IsRejected || IsResolved) // ignore if already rejected or resolved
                return this;
            IsResolved = true;
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
            if (IsRejected || IsResolved) // ignore if already rejected or resolved
                return this;
            IsResolved = true;
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
            foreach (var callback in _callbacks)
            {
                if (callback.Cond == cond || callback.Cond == Callback.Condition.Always)
                {
                    if (callback.IsReturnValue)
                        callback.Del.DynamicInvoke(_arg);
                    else
                        callback.Del.DynamicInvoke();
                }
            }
            _callbacks.Clear();
        }
    }
}
