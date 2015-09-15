using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Caesar.Promise.Test
{
    [TestClass]
    public class PromiseTest
    {
        [TestMethod]
        public void ResolveTest()
        {
            var deferred = new Deferred<string, int>();

            bool alwaysHit = false;
            dynamic alwaysArg = null;

            bool doneHit = false;
            string doneArg = null;

            bool failHit = false;

            var promise = deferred.Promise();
            promise.Always((arg) =>
            {
                alwaysHit = true;
                alwaysArg = arg;
            });
            promise.Done((arg) =>
            {
                doneHit = true;
                doneArg = arg;
            });
            promise.Fail(() => failHit = true);

            deferred.Resolve("test");

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.AreEqual("test", alwaysArg);

            Assert.IsTrue(doneHit, "Done is hit");
            Assert.AreEqual("test", doneArg);

            Assert.IsFalse(failHit, "Fail is not hit");
        }

        [TestMethod]
        public void RejectTest()
        {
            var deferred = new Deferred<string, int>();

            bool alwaysHit = false;
            dynamic alwaysArg = null;

            bool doneHit = false;

            bool failHit = false;
            int? failArg = null;

            var promise = deferred.Promise();
            promise.Always((arg) =>
            {
                alwaysHit = true;
                alwaysArg = arg;
            });
            promise.Done(() => doneHit = true);
            promise.Fail((arg) =>
            {
                failHit = true;
                failArg = arg;
            });

            deferred.Reject(1337);

            Assert.IsTrue(alwaysHit, "Always is hit");
            Assert.AreEqual(alwaysArg, 1337);

            Assert.IsFalse(doneHit, "Done is not hit");

            Assert.IsTrue(failHit, "Fail is hit");
            Assert.AreEqual(failArg, 1337);
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
