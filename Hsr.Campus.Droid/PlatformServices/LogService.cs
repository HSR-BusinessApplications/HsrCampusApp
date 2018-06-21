// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System;
    using System.Diagnostics;
    using Core.ApplicationServices;

    public class LogService
        : ILogging
    {
        public void Exception(object sender, Exception ex)
        {
            Debug.WriteLine(">Exception ({0}): {1}".FormatWith(sender, ex.Message));
        }

        public void Info(object sender, string message, params object[] parameters)
        {
            Debug.WriteLine(">Info ({0}): {1}".FormatWith(sender, message.FormatWith(parameters)));
        }

        public void Warning(object sender, string warning, params object[] parameters)
        {
            Debug.WriteLine(">Warn ({0}): {1}".FormatWith(sender, warning.FormatWith(parameters)));
        }
    }
}
