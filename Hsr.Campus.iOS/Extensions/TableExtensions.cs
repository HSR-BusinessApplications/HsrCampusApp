// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using UIKit;

    public static class TableExtensions
    {
        public static void TrimEmptyCells(this UITableView table)
        {
            table.TableFooterView = new UIView(CoreGraphics.CGRect.Empty);
        }
    }
}
