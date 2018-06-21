// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System;

    public interface ILogging
    {
        void Exception(object sender, Exception ex);

        void Info(object sender, string message, params object[] parameters);

        void Warning(object sender, string warning, params object[] parameters);
    }
}
