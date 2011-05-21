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
            throw new NotImplementedException();
        }

        public Deferred done(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Deferred done(IEnumerable<Delegate> callbacks)
        {
            throw new NotImplementedException();
        }

        public Deferred fail(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public Deferred fail(IEnumerable<Delegate> callbacks)
        {
            throw new NotImplementedException();
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

        public Deferred then(Delegate doneCallback, Delegate failCallback)
        {
            throw new NotImplementedException();
        }

        public Deferred then(IEnumerable<Delegate> doneCallback, IEnumerable<Delegate> failCallback)
        {
            throw new NotImplementedException();
        }
    }
}
