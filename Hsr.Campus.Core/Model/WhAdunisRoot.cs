// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhAdunisRoot
    {
        [DataMember]
        public IEnumerable<WhAdunisCourse> Courses { get; set; }

        [DataMember]
        public string Person { get; set; }

        [DataMember]
        public string Semester { get; set; }
    }
}
