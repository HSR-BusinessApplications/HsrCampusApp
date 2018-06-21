// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Room")]
    public class WhAdunisRoom
    {
        [DataMember(Name = "Nummer")]
        public string Number { get; set; }

        public override string ToString() => this.Number;
    }
}
