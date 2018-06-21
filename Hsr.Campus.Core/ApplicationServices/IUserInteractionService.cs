// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System;

    public interface IUserInteractionService
    {
        void Notification(string title, string content, Action after);

        void Toast(string content, ToastTime showFor);

        void Dialog(string title, string content, Action afterOk = null, Action afterCancel = null);
    }
}
