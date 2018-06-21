// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Resources
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public static class PublicAppResources
    {
        public static Dictionary<string, string> ThirdPartyLicenses
        {
            get
            {
                var licenses = new Dictionary<string, string>();
                var resourceSet = Licenses.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                foreach (DictionaryEntry entry in resourceSet)
                {
                    licenses.Add(entry.Key.ToString(), entry.Value.ToString());
                }

                return licenses;
            }
        }
    }
}
