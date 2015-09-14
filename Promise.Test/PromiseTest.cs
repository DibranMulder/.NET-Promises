using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Promise.Test
{
    [TestClass]
    public class PromiseTest
    {
        [TestMethod]
        public void ResolveTest()
        {
            var deferred = new Deferred();

            bool alwaysHit = false;
            bool doneHit = false;
            bool failHit = false;

            var promise = deferred.Promise();
            promise.Always(() => alwaysHit = true);
            promise.Done(() => doneHit = true);
            promise.Fail(() => failHit = true);

            deferred.Resolve();

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

            var promise = deferred.Promise();
            promise.Always(() => alwaysHit = true);
            promise.Done(() => doneHit = true);
            promise.Fail(() => failHit = true);

            deferred.Reject();

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.IsFalse(doneHit, "Done is not hit");
            Assert.IsTrue(failHit, "Fail is hit");
        }
        
        [TestMethod]
        public void ResolveFinishedTest()
        {
            var deferred = new Deferred();
            deferred.Resolve();
            var promise = deferred.Promise();

            bool alwaysHit = false;
            bool doneHit = false;
            bool failHit = false;

            promise.Always(() => alwaysHit = true);
            promise.Done(() => doneHit = true);
            promise.Fail(() => failHit = true);

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.IsTrue(doneHit, "Done is hit");
            Assert.IsFalse(failHit, "Fail is not hit");
        }

        [TestMethod]
        public void RejectFinishedTest()
        {
            var deferred = new Deferred();
            deferred.Reject();
            var promise = deferred.Promise();

            bool alwaysHit = false;
            bool doneHit = false;
            bool failHit = false;

            promise.Always(() => alwaysHit = true);
            promise.Done(() => doneHit = true);
            promise.Fail(() => failHit = true);

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.IsFalse(doneHit, "Done is not hit");
            Assert.IsTrue(failHit, "Fail is hit");
        }
    }
}
