// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Lecturer")]
    public class WhAdunisLecturer
    {
        [DataMember]
        public string Fullname { get; set; }

        [DataMember]
        public string Shortname { get; set; }

        public override string ToString() => this.Shortname;
    }
}
