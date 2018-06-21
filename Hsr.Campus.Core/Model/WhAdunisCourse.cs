// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Course")]
    public class WhAdunisCourse
    {
        /// <summary>
        /// Gets or sets the official description of the appointment
        /// Example: Jap2-v1
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<WhAdunisCourseAllocation> CourseAllocations { get; set; }
    }
}
