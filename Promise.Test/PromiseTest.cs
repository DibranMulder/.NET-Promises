using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Promise.Test
{
    [TestClass]
    public class PromiseTest
    {
        private delegate void Callback();

        [TestMethod]
        public void ResolveTest()
        {
            var deferred = new Deferred();

            bool alwaysHit = false;
            bool doneHit = false;
            bool failHit = false;

            var promise = deferred.promise();
            promise.always(new Callback(() => alwaysHit = true));
            promise.done(new Callback(() => doneHit = true));
            promise.fail(new Callback(() => failHit = true));

            deferred.resolve();

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.IsTrue(doneHit, "Done is hit");
            Assert.IsFalse(failHit, "Fail is not hit");
        }

        [TestMethod]
        public void RejectTest()
        {
            var deferred = new Deferred();

            bool alwaysHit = false;
            bool doneHit = false;
            bool failHit = false;

            var promise = deferred.promise();
            promise.always(new Callback(() => alwaysHit = true));
            promise.done(new Callback(() => doneHit = true));
            promise.fail(new Callback(() => failHit = true));

            deferred.reject();

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.IsFalse(doneHit, "Done is not hit");
            Assert.IsTrue(failHit, "Fail is hit");
        }
    }
}
