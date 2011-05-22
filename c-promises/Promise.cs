using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_promises
{
    public interface Promise
    {
        Promise always(Delegate callback);
        Promise always(IEnumerable<Delegate> callbacks);

        Promise done(Delegate callback);
        Promise done(IEnumerable<Delegate> callbacks);

        Promise fail(Delegate callback);
        Promise fail(IEnumerable<Delegate> callbacks);

        bool isRejected();
        bool isResolved();

        Promise pipe(Delegate doneFilter, Delegate failFilter);

        Promise then(Delegate doneCallback, Delegate failCallback);
        Promise then(IEnumerable<Delegate> doneCallbacks, IEnumerable<Delegate> failCallbacks);
    }
}
