using System;
using NUnit.Framework;

namespace NoCommons.Tests
{
    public class AssertionUtils
    {
        public static void AssertMessageContains(ArgumentException argumentException, string errorSyntax)
        {
            bool containsText = argumentException.Message.Contains(errorSyntax);
            Assert.IsTrue(containsText);
        }
    }
}