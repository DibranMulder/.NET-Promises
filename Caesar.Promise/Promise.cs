using System;
using System.Collections.Generic;

namespace Caesar.Promise
{
    /// <summary>
    /// .Net implementation of JavaScript / JQuery promises.
    /// </summary>
    public interface IPromise
    {
        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        IPromise Done(Action callback);

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        IPromise Fail(Action callback);

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        IPromise Always(Action callback);

        /// <summary>
        /// States if the promise is rejected.
        /// </summary>
        bool IsRejected { get; }

        /// <summary>
        /// States if the promise is resolved.
        /// </summary>
        bool IsResolved { get; }

        /// <summary>
        /// States if the promise is fulfilled.
        /// </summary>
        bool IsFulfilled { get; }
    }

    /// <summary>
    /// Generic .Net implementation of JavaScript / JQuery promises.
    /// </summary>
    /// <typeparam name="T">The type of Resolve argument that gets passed into the Done callbacks.</typeparam>
    /// <typeparam name="TFail">The type of the Reject argument that gets passed into the Fail callbacks.</typeparam>
    public interface IPromise<out T, out TFail> : IPromise
    {
        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        IPromise<T, TFail> Done(Action<T> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        IPromise<T, TFail> Done(IEnumerable<Action<T>> callbacks);

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        IPromise<T, TFail> Fail(Action<TFail> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callbacks">The callbacks.</param>
        /// <returns>
        /// Itself
        /// </returns>
        IPromise<T, TFail> Fail(IEnumerable<Action<TFail>> callbacks);

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        IPromise<T, TFail> Always(Action<dynamic> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        IPromise<T, TFail> Always(IEnumerable<Action<dynamic>> callbacks);
    }
}
