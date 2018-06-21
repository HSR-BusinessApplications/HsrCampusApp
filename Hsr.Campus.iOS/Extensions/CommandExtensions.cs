// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Windows.Input;

    public static class CommandExtensions
    {
        public static EventHandler ToEventHandler(this ICommand command, object parameter = null) => (o, e) => command.Execute(parameter);

        public static EventHandler ToEventHandler(this Action action) => (o, e) => action();
    }
}
