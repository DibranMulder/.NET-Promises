using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_promises
{
    class Promise
    {
        public Promise then(Delegate doneCallback = null, Delegate failCallback = null)
        {
            throw new NotImplementedException();
        }

        public Promise then(IEnumerable<Delegate> doneCallbacks, IEnumerable<Delegate> failCallbacks)
        {
            foreach (Delegate doneCallback in doneCallbacks)
            {
                this.then(doneCallback, null);
            }
            foreach (Delegate failCallback in failCallbacks)
            {
                this.then(null, failCallback);
            }
            return this;
        }

        public Promise done(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise done(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.done(callback);
            }
            return this;
        }

        public Promise fail(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise fail(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.fail(callback);
            }
            return this;
        }

        public Promise always(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Promise always(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.always(callback);
            }
            return this;
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