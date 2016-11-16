using System.Collections;
using NUnit.Framework;
using Task3.Logic;

namespace Task3.Tests
{
    [TestFixture]
    public class SetTests
    {
        private readonly Set<string> _stringSet1 = 
            new Set<string>(5, "Yeah", "cat", "Wow!");
        private readonly Set<string> _stringSet2 =
            new Set<string>(4, "People", "Joy", "String");

        private const string StringInsert = "hello";
        private const string StringErase = "String";

        private static readonly Set<string> ExpextedSet1 =
            new Set<string>(5, "Yeah", "cat", "Wow!", "hello");
        private static readonly Set<string> ExpextedSet2 =
            new Set<string>(4, "People", "Joy");

        [Test, TestCaseSource(typeof(StringInsertTest), 
            nameof(StringInsertTest.TestCasesInsert))]
        public Set<string> StringInsertTests(string element)
        {
            _stringSet1.Insert(element);
            return _stringSet1;
        }

        [Test, TestCaseSource(typeof(StringEraseTest),
           nameof(StringEraseTest.TestCasesErase))]
        public Set<string> StringEraseTests(string element)
        {
            _stringSet2.Erase(element);
            return _stringSet2;
        }

        public class StringInsertTest
        {
            public static IEnumerable TestCasesInsert
            {
                get
                {
                    yield return new TestCaseData(StringInsert).Returns(ExpextedSet1);
                }
            }
        }

        public class StringEraseTest
        {
            public static IEnumerable TestCasesErase
            {
                get
                {
                    yield return new TestCaseData(StringErase).Returns(ExpextedSet2);
                }
            }
        }
    }
}
