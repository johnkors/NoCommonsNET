using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NoCommons.Org;

namespace NoCommons.Tests.Org
{
    public class OrganisasjonsnummerValidatorTests
    {
        private const string ORGNUMMER_VALID = "981566378";
        private const string ORGNUMMER_INVALID_CHECKSUM = "123456789";

        [Test]
        public void testInvalidOrgnummerWrongLength()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateSyntax("0123456789");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidOrgnummerNotDigits()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateSyntax("abcdefghijk");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidOrgnummerWrongChecksum()
        {
            try
            {
                OrganisasjonsnummerValidator.ValidateChecksum(ORGNUMMER_INVALID_CHECKSUM);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, OrganisasjonsnummerValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [Test]
        public void testGetValidOrgnummerFromInvalidOrgnummerWrongChecksum()
        {
            Organisasjonsnummer orgNr = OrganisasjonsnummerValidator.GetAndForceValidOrganisasjonsnummer(ORGNUMMER_INVALID_CHECKSUM);
            Assert.IsTrue(OrganisasjonsnummerValidator.IsValid(orgNr.ToString()));
        }

        [Test]
        public void testIsValid()
        {
            Assert.IsTrue(OrganisasjonsnummerValidator.IsValid(ORGNUMMER_VALID));
            Assert.IsFalse(OrganisasjonsnummerValidator.IsValid(ORGNUMMER_INVALID_CHECKSUM));
        }
    }
}