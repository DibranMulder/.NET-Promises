using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promise
{
    public interface Promise
    {
        Promise Done(Action callback);
        Promise Fail(Action callback);
        Promise Always(Action callback);

        bool IsRejected { get; }
        bool IsResolved { get; }
        bool IsFulfilled { get; }
    }

    public interface Promise<T> : Promise
    {
        Promise<T> Done(Action<T> callback);
        Promise<T> Done(IEnumerable<Action<T>> callbacks);

        Promise<T> Fail(Action<T> callback);
        Promise<T> Fail(IEnumerable<Action<T>> callbacks);

        Promise<T> Always(Action<T> callback);
        Promise<T> Always(IEnumerable<Action<T>> callbacks);
    }
}
