using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using NoCommons.Org;

namespace NoCommons.Tests.Org
{
    public class OrganisasjonsnummerValidatorTests
    {
        private const string ORGNUMMER_VALID = "981566378";
        private const string ORGNUMMER_INVALID_CHECKSUM = "123456789";

        [Fact]
        public void testInvalidOrgnummerWrongLength()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateSyntax("0123456789");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Fact]
        public void testInvalidOrgnummerNotDigits()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateSyntax("abcdefghijk");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Fact]
        public void testInvalidOrgnummerWrongChecksum()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateChecksum(ORGNUMMER_INVALID_CHECKSUM);
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [Fact]
        public void testGetValidOrgnummerFromInvalidOrgnummerWrongChecksum()
        {
            Organisasjonsnummer orgNr = OrganisasjonsnummerValidator.GetAndForceValidOrganisasjonsnummer(ORGNUMMER_INVALID_CHECKSUM);
            Assert.True(OrganisasjonsnummerValidator.IsValid(orgNr.ToString()));
        }

        [Fact]
        public void testIsValid()
        {
            Assert.True(OrganisasjonsnummerValidator.IsValid(ORGNUMMER_VALID));
            Assert.False(OrganisasjonsnummerValidator.IsValid(ORGNUMMER_INVALID_CHECKSUM));
        }
    }
}