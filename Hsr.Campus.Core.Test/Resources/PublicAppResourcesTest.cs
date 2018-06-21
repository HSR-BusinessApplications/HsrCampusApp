// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.Resources
{
    using System.Linq;
    using Hsr.Campus.Core.Resources;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PublicAppResourcesTest
    {
        [TestMethod]
        public void TestThirdPartyLicenses()
        {
            var thirdPartyLicenses = PublicAppResources.ThirdPartyLicenses;

            Assert.IsTrue(thirdPartyLicenses.Count > 0);

            Assert.IsTrue(thirdPartyLicenses.FirstOrDefault().Key.Length > 0);
            Assert.IsTrue(thirdPartyLicenses.FirstOrDefault().Value.Length > 0);
        }
    }
}
