using System;
using Xunit;

namespace NoCommons.Tests
{
    public class AssertionUtils
    {
        public static void AssertMessageContains(ArgumentException argumentException, string errorSyntax)
        {
            bool containsText = argumentException.Message.Contains(errorSyntax);
            Assert.True(containsText);
        }

        
    }
}