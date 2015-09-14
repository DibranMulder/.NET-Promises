using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promise
{
    class Callback
    {
        public enum Condition { Always, Success, Fail };

        private Delegate del;
        private Condition cond;

        public Callback(Delegate del, Condition cond)
        {
            this.del = del;
            this.cond = cond;
        }

        public Delegate Del
        {
            get { return del; }
        }

        public Condition Cond
        {
            get { return cond; }
        }
    }
}
