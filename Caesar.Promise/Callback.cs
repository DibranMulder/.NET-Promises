using System;

namespace Caesar.Promise
{
    /// <summary>
    /// An internal class that is used as a bookkeeping of registered callbacks.
    /// </summary>
    internal class Callback
    {
        /// <summary>
        /// States a fulfilment condition for the registered callback.
        /// </summary>
        internal enum Condition
        {
            Always,
            Success,
            Fail
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Callback"/> class.
        /// </summary>
        /// <param name="del">The delete.</param>
        /// <param name="cond">The cond.</param>
        /// <param name="returnValue">if set to <c>true</c> [return value].</param>
        public Callback(Delegate del, Condition cond, bool returnValue)
        {
            Del = del;
            Cond = cond;
            IsReturnValue = returnValue;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is return value.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is return value; otherwise, <c>false</c>.
        /// </value>
        public bool IsReturnValue { get; private set; }

        /// <summary>
        /// Gets the delete.
        /// </summary>
        /// <value>
        /// The delete.
        /// </value>
        public Delegate Del { get; private set; }

        /// <summary>
        /// Gets the cond.
        /// </summary>
        /// <value>
        /// The cond.
        /// </value>
        public Condition Cond { get; private set; }
    }
}
