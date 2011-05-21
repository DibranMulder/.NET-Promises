using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_promises
{
    class Promise
    {
        public Promise then(Delegate doneCallback, Delegate failCallback)
        {
            throw new NotImplementedException();
        }

        public Promise then(IEnumerable<Delegate> doneCallback, IEnumerable<Delegate> failCallback)
        {
            throw new NotImplementedException();
        }

        public Promise done(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise done(IEnumerable<Delegate> callbacks)
        {
            throw new NotImplementedException();
        }

        public Promise fail(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise fail(IEnumerable<Delegate> callbacks)
        {
            throw new NotImplementedException();
        }

        public Promise always(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise always(IEnumerable<Delegate> callbacks)
        {
            throw new NotImplementedException();
        }

        public Promise pipe(Delegate doneFilter = null, Delegate failFilter = null)
        {
            throw new NotImplementedException();
        }

        public Promise isResolved()
        {
            throw new NotImplementedException();
        }

        public Promise isRejected()
        {
            throw new NotImplementedException();
        }
    }
}