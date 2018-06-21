// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using MvvmCross.Platform;

#pragma warning disable SA1401 // Fields must be private
    /// <summary>
    /// Defines the ValueConverter for MvvmCross
    /// </summary>
    /// <remarks>
    /// The converters have to be public fields. Properties or private fields do not work. (MvvmCross v4.2.3)
    /// </remarks>
    public class Converters
    {
        public readonly FbImageConverter FbImage = Mvx.IocConstruct<FbImageConverter>();

        public readonly NegateConverter Negate = new NegateConverter();

        public readonly DateTimeRelativeConverter DateTimeRelative = new DateTimeRelativeConverter();

        public readonly BalanceDepositConverter BalanceDeposit = new BalanceDepositConverter();

        public readonly BalanceUpdateConverter BalanceUpdate = new BalanceUpdateConverter();

        public readonly FilerIconConverter FilerIcon = new FilerIconConverter();

        public readonly BundleIconConverter BundleIcon = new BundleIconConverter();

        public readonly LocalTimeConverter LocalTime = new LocalTimeConverter();

        public readonly AppointmentTimeConverter AppointmentTime = new AppointmentTimeConverter();

        public readonly TimeperiodConverter Timeperiod = new TimeperiodConverter();

        public readonly AdunisIconConverter AdunisIcon = new AdunisIconConverter();

        public readonly ResConverter Res = Mvx.IocConstruct<ResConverter>();

        public readonly IsEmptyConverter IsEmpty = new IsEmptyConverter();

        public readonly FilerLocalConverter FilerLocal = new FilerLocalConverter();
    }
#pragma warning restore SA1401 // Fields must be private
}
