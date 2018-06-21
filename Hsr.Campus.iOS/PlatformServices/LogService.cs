// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System;
    using Hsr.Campus.Core.ApplicationServices;

    public class LogService
        : ILogging
    {
        public void Exception(object sender, Exception ex)
        {
        }

        public void Info(object sender, string message, params object[] parameters)
        {
        }

        public void Warning(object sender, string warning, params object[] parameters)
        {
        }
    }
}
