using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_promises
{
    public class Deferred
    {
        public Deferred always(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Deferred always(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.always(callback);
            }
            return this;
        }

        public Deferred done(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Deferred done(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.done(callback);
            }
            return this;
        }

        public Deferred fail(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Deferred fail(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.fail(callback);
            }
            return this;
        }

        public Boolean isRejected()
        {
            throw new NotImplementedException();
        }

        public Boolean isResolved()
        {
            throw new NotImplementedException();
        }

        public Deferred pipe(Delegate doneFilter = null, Delegate failFilter = null)
        {
            throw new NotImplementedException();
        }

        public Promise promise()
        {
            throw new NotImplementedException();
        }

        public Deferred reject()
        {
            throw new NotImplementedException();
        }

        public Deferred reject<T>(T arg)
        {
            throw new NotImplementedException();
        }

        public Deferred resolve()
        {
            throw new NotImplementedException();
        }

        public Deferred resolve<T>(T arg)
        {
            throw new NotImplementedException();
        }

        public Deferred then(Delegate doneCallback = null, Delegate failCallback = null)
        {
            throw new NotImplementedException();
        }

        public Deferred then(IEnumerable<Delegate> doneCallbacks, IEnumerable<Delegate> failCallbacks)
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
    }
}
