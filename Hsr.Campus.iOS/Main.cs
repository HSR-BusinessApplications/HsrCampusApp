// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System.Diagnostics.CodeAnalysis;
    using UIKit;

#pragma warning disable RCS1102 // Make class static.
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name must match first type name", Justification = "Application could be a special name.")]

    public class Application
#pragma warning restore RCS1102 // Make class static.
    {
        public static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
