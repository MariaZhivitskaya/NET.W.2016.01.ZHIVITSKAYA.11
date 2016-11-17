using System.Collections;
using NUnit.Framework;
using Task2.Logic;

namespace Task2.Tests
{
    [TestFixture]
    public class QueueTests
    {
        private readonly Queue<int> _intQueue = 
            new Queue<int>(10, 10, 5, -5, 4, 10, 4, 4);
        private readonly Queue<string> _stringQueue =
            new Queue<string>(5, "calm", "street", "only");

        private const int PushInt = 18;
        private const int PopInt = 10;
        private const string PushString = "AA";
        private const string PopString = "calm";

        private static readonly Queue<int> ExpectedQueue1 = 
            new Queue<int>(10, 10, 5, -5, 4, 10, 4, 4, 18);
        private static readonly Queue<int> ExpectedQueue2 =
            new Queue<int>(10, 5, -5, 4, 10, 4, 4);
        private static readonly Queue<string> ExpectedQueue3 =
            new Queue<string>(5, "calm", "street", "only", "AA");
        private static readonly Queue<string> ExpectedQueue4 =
            new Queue<string>(5, "street", "only");


        [Test, TestCaseSource(typeof(PushIntTest),
            nameof(PushIntTest.TestCasesPush))]
        public Queue<int> PushIntTests(int element)
        {
            _intQueue.Push(element);
            return _intQueue;
        }

        [Test, TestCaseSource(typeof (PopIntTest),
            nameof(PopIntTest.TestCasesPop))]
        public int PopIntTests()
        {
            int elelemt = _intQueue.Pop();
            Assert.AreEqual(_intQueue, ExpectedQueue2);
            return elelemt;
        } 

        [Test, TestCaseSource(typeof(PushStringTest),
            nameof(PushStringTest.TestCasesPush))]
        public Queue<string> PushStringTests(string element)
        {
            _stringQueue.Push(element);
            return _stringQueue;
        }

        [Test, TestCaseSource(typeof (PopStringTest),
            nameof(PopStringTest.TestCasesPop))]
        public string PopStringTests()
        {
            string elelemt = _stringQueue.Pop();
            Assert.AreEqual(_stringQueue, ExpectedQueue4);
            return elelemt;
        }

        public class PushIntTest
        {
            public static IEnumerable TestCasesPush
            {
                get
                {
                    yield return new TestCaseData(PushInt).Returns(ExpectedQueue1);
                }
            }
        }

        public class PopIntTest
        {
            public static IEnumerable TestCasesPop
            {
                get
                {
                    yield return new TestCaseData().Returns(PopInt);
                }
            }
        }

        public class PushStringTest
        {
            public static IEnumerable TestCasesPush
            {
                get
                {
                    yield return new TestCaseData(PushString).Returns(ExpectedQueue3);
                }
            }
        }

        public class PopStringTest
        {
            public static IEnumerable TestCasesPop
            {
                get
                {
                    yield return new TestCaseData().Returns(PopString);
                }
            }
        }
    }
}
