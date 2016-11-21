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
        private static readonly Set<string> StringSet2 = new Set<string>();

        private const string StringIntersect = "cat";
        private const string StringInsert = "hello";
        private const string StringErase = "cat";
        private const string ErrorStringErase = "String";

        private static readonly Set<string> ExpextedUnionSet;
        private static readonly Set<string> ExpextedIntersectionSet;
        private static readonly Set<string> ExpextedExceptionSet;
        private static readonly Set<string> ExpextedSymmetricExceptionSet;
        private static readonly Set<string> ExpextedSetWithInsertion;
        private static readonly Set<string> ExpextedSetWithErasing = new Set<string>();

        static SetTests()
        {
            StringSet1.Insert("Yeah");
            StringSet1.Insert("cat");
            StringSet1.Insert("Wow!");
            StringSet1.Insert("walk");
            StringSet1.Insert("walk");
            StringSet1.Insert("empty");

            StringSet2.Insert("beauty");
            StringSet2.Insert("Star");
            StringSet2.Insert("Chocolate");
            StringSet2.Insert("cat");

            ExpextedUnionSet = StringSet1.Clone();
            foreach (var element in StringSet2)
                ExpextedUnionSet.Insert(element);

            ExpextedIntersectionSet = new Set<string>();
            ExpextedIntersectionSet.Insert(StringIntersect);

            ExpextedExceptionSet = StringSet1.Clone();
            ExpextedExceptionSet.Erase(StringIntersect);

            ExpextedSymmetricExceptionSet = ExpextedUnionSet;
            ExpextedUnionSet.Erase(StringIntersect);

            ExpextedSetWithInsertion = StringSet1.Clone();
            ExpextedSetWithInsertion.Insert("hello");
            
            ExpextedSetWithErasing.Insert("Yeah");
            ExpextedSetWithErasing.Insert("Wow!");
            ExpextedSetWithErasing.Insert("walk");
            ExpextedSetWithErasing.Insert("empty");
        }

        [Test, TestCaseSource(typeof(StringUnionTest),
            nameof(StringUnionTest.TestCasesUnion))]
        public Set<string> StringUnionTests(Set<string> set1, Set<string> set2) =>
            Set<string>.Union(set1, set2);

        [Test, TestCaseSource(typeof (StringIntersectionTest),
            nameof(StringIntersectionTest.TestCasesIntersect))]
        public Set<string> StringIntersectionTests(Set<string> set1, Set<string> set2) =>
            Set<string>.Intersect(set1, set2);

        [Test, TestCaseSource(typeof(StringExceptionTest),
            nameof(StringExceptionTest.TestCasesExcept))]
        public Set<string> StringExcaptionTests(Set<string> set1, Set<string> set2) =>
            Set<string>.Except(set1, set2);

        [Test, TestCaseSource(typeof(StringSymmetricExceptionTest),
          nameof(StringSymmetricExceptionTest.TestCasesSymmetricExcept))]
        public Set<string> StringSymmetricExcaptionTests(Set<string> set1, Set<string> set2) =>
          Set<string>.SymmetricExcept(set1, set2);

        [Test, TestCaseSource(typeof(StringInsertionTest), 
            nameof(StringInsertionTest.TestCasesInsert))]
        public Set<string> StringInsertTests(string element)
        {
            StringSet1.Insert(element);
            return StringSet1;
        }

        [Test, TestCaseSource(typeof(StringErasingTest),
           nameof(StringErasingTest.TestCasesErase))]
        public Set<string> StringEraseTests(string element)
        {
            StringSet1.Erase(element);
            return StringSet1;
        }

        public class StringUnionTest
        {
            public static IEnumerable TestCasesUnion
            {
                get
                {
                    yield return new TestCaseData(StringSet1, StringSet2).
                        Returns(ExpextedUnionSet);
                }
            }
        }

        public class StringIntersectionTest
        {
            public static IEnumerable TestCasesIntersect
            {
                get
                {
                    yield return new TestCaseData(StringSet1, StringSet2).
                        Returns(ExpextedIntersectionSet);
                }
            }
        }

        public class StringExceptionTest
        {
            public static IEnumerable TestCasesExcept
            {
                get
                {
                    yield return new TestCaseData(StringSet1, StringSet2).
                        Returns(ExpextedExceptionSet);
                }
            }
        }

        public class StringSymmetricExceptionTest
        {
            public static IEnumerable TestCasesSymmetricExcept
            {
                get
                {
                    yield return new TestCaseData(StringSet1, StringSet2).
                        Returns(ExpextedSymmetricExceptionSet);
                }
            }
        }

        public class StringInsertionTest
        {
            public static IEnumerable TestCasesInsert
            {
                get
                {
                    yield return new TestCaseData(StringInsert).Returns(ExpextedSetWithInsertion);
                }
            }
        }
        
        public class StringErasingTest
        {
            public static IEnumerable TestCasesErase
            {
                get
                {
                    yield return new TestCaseData(StringErase).Returns(ExpextedSetWithErasing);
                    yield return new TestCaseData(ErrorStringErase)
                        .Throws(typeof(ArgumentNullException));
                }
            }
        }
    }
}
