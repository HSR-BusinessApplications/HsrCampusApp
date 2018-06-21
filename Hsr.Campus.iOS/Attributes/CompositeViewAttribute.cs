// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using MvvmCross.Platform.IoC;

    [AttributeUsage(AttributeTargets.Class)]
    public class CompositeViewAttribute
        : MvxConditionalConventionalAttribute
    {
        public override bool IsConditionSatisfied => false;
    }
}
