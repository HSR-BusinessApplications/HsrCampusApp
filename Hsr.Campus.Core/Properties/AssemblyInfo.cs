// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Properties
{
    public static class AssemblyInfo
    {
        public const string AssemblyVersion = "3.1.0";
        public const string AssemblyFileVersion = "3.1.2019.0214";
        public const string AssemblyInformationalVersion = AssemblyFileVersion + "-" + Compile;
        public const string Company = "Hochschule für Technik Rapperswil";
        public const string Copyright = "Copyright © HSR 2019";

#if TEST_DATA
        private const string Compile = "test";
#elif DEBUG
        private const string Compile = "debug";
#else
        private const string Compile = "release";
#endif
    }
}
