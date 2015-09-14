using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promise
{
    public class Deferred : Promise
    {
        private List<Callback> callbacks = new List<Callback>();
        private bool _isResolved = false;
        private bool _isRejected = false;
        private object[] args = null;

        public Deferred always(Delegate callback)
        {
            callbacks.Add(new Callback(callback, Callback.Condition.Always));
            return this;
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
            callbacks.Add(new Callback(callback, Callback.Condition.Success));
            return this;
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
            callbacks.Add(new Callback(callback, Callback.Condition.Fail));
            return this;
        }

        public Deferred fail(IEnumerable<Delegate> callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                this.fail(callback);
            }
            return this;
        }

        public bool isRejected()
        {
            return this._isRejected;
        }

        public bool isResolved()
        {
            return this._isResolved;
        }

        public Deferred pipe(Delegate doneFilter = null, Delegate failFilter = null)
        {
            throw new NotImplementedException();
        }

        public Promise promise()
        {
            return this;
        }

        public Deferred reject()
        {
            _isRejected = true;
            DequeueCallbacks(Callback.Condition.Fail);

            return this;
        }

        public Deferred reject(object[] args)
        {
            _isRejected = true;
            this.args = args;
            DequeueCallbacks(Callback.Condition.Fail);

            return this;
        }

        public Deferred resolve()
        {
            this._isResolved = true;
            DequeueCallbacks(Callback.Condition.Success);

            return this;
        }

        public Deferred resolve(object[] args)
        {
            this._isResolved = true;
            this.args = args;
            DequeueCallbacks(Callback.Condition.Success);

            return this;
        }

        public Deferred then(Delegate doneCallback = null, Delegate failCallback = null)
        {
            if (doneCallback != null) { callbacks.Add(new Callback(doneCallback, Callback.Condition.Success)); }
            if (failCallback != null) { callbacks.Add(new Callback(failCallback, Callback.Condition.Fail)); }

            return this;
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

        private void DequeueCallbacks(Callback.Condition cond)
        {
            foreach (Callback callback in callbacks)
            {
                if (callback.Cond == cond || callback.Cond == Callback.Condition.Always)
                {
                    callback.Del.DynamicInvoke(args);
                }
            }
            callbacks.Clear();
        }

        #region Promise Accessors
        // We need to also implement accessors using the Promise Interface because unfortunately .NET doesn't support covariant return types
        // http://stackoverflow.com/questions/1121283/interface-not-implemented-when-returning-derived-type
        Promise Promise.always(Delegate callback) { return (Promise)always(callback); }
        Promise Promise.always(IEnumerable<Delegate> callbacks) { return (Promise)always(callbacks); }
        Promise Promise.done(Delegate callback) { return (Promise)done(callback); }
        Promise Promise.done(IEnumerable<Delegate> callbacks) { return (Promise)done(callbacks); }
        Promise Promise.fail(Delegate callback) { return (Promise)fail(callback); }
        Promise Promise.fail(IEnumerable<Delegate> callbacks) { return (Promise)fail(callbacks); }
        Promise Promise.pipe(Delegate doneFilter, Delegate failFilter) { return (Promise)pipe(doneFilter, failFilter); }
        Promise Promise.then(Delegate doneCallback, Delegate failCallback) { return (Promise)then(doneCallback, failCallback); }
        Promise Promise.then(IEnumerable<Delegate> doneCallbacks, IEnumerable<Delegate> failCallbacks) { return (Promise)then(doneCallbacks, failCallbacks); }
        #endregion
    }
}
