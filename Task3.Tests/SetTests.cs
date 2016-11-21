using System;
using System.Collections;
using NUnit.Framework;
using Task3.Logic;

namespace Task3.Tests
{
    [TestFixture]
    public class SetTests
    {
        private static readonly Set<string> StringSet1 = new Set<string>();

        private const string StringInsert = "hello";
        private const string StringErase = "cat";
        private const string ErrorStringErase = "String";

        private static readonly Set<string> ExpextedSet1 = new Set<string>(); 
        private static readonly Set<string> ExpextedSet2 = new Set<string>();

        static SetTests()
        {
            StringSet1.Insert("Yeah");
            StringSet1.Insert("cat");
            StringSet1.Insert("Wow!");
            StringSet1.Insert("walk");
            StringSet1.Insert("walk");
            StringSet1.Insert("empty");
            
            ExpextedSet1.Insert("Yeah");
            ExpextedSet1.Insert("cat");
            ExpextedSet1.Insert("Wow!");
            ExpextedSet1.Insert("walk");
            ExpextedSet1.Insert("empty");
            ExpextedSet1.Insert("hello");

            ExpextedSet2.Insert("Yeah");
            ExpextedSet2.Insert("Wow!");
            ExpextedSet2.Insert("walk");
            ExpextedSet2.Insert("empty");
        }

        [Test, TestCaseSource(typeof(StringInsertTest), 
            nameof(StringInsertTest.TestCasesInsert))]
        public Set<string> StringInsertTests(string element)
        {
            StringSet1.Insert(element);
            return StringSet1;
        }

        [Test, TestCaseSource(typeof(StringEraseTest),
           nameof(StringEraseTest.TestCasesErase))]
        public Set<string> StringEraseTests(string element)
        {
            StringSet1.Erase(element);
            return StringSet1;
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
                    yield return new TestCaseData(ErrorStringErase)
                        .Throws(typeof(ArgumentNullException));
                }
            }
        }
    }
}
