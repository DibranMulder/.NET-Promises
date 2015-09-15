using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesar.Promise
{
    class Callback
    {
        public enum Condition { Always, Success, Fail };

        public Callback(Delegate del, Condition cond, bool returnValue)
        {
            Del = del;
            Cond = cond;
            IsReturnValue = returnValue;
        }

        public bool IsReturnValue { get; private set; }
        public Delegate Del { get; private set; }
        public Condition Cond { get; private set; }

    }
}
