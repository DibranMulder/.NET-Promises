using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesar.Promise
{
    public interface Promise
    {
        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        Promise Done(Action callback);

        /// <summary>
        /// Adds a callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        Promise Fail(Action callback);

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A callback of type action</param>
        /// <returns>Itself</returns>
        Promise Always(Action callback);

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

    public interface Promise<T, TFail> : Promise
    {
        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Done(Action<T> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Done(IEnumerable<Action<T>> callbacks);

        /// <summary>
        /// Adds a generic callback to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Fail(Action<TFail> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment but only when its rejected.
        /// </summary>
        /// <param name="callback">A generic list of callbacks of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Fail(IEnumerable<Action<TFail>> callbacks);

        /// <summary>
        /// Adds a callback to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callback">A generic callback of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Always(Action<dynamic> callback);

        /// <summary>
        /// Adds a generic list of callbacks to the promised fulfillment regardless if it gets rejected or resolved.
        /// </summary>
        /// <param name="callbacks">A generic list of callsbacks of type action</param>
        /// <returns>Itself</returns>
        Promise<T, TFail> Always(IEnumerable<Action<dynamic>> callbacks);
    }
}
