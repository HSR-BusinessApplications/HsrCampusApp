// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    public enum AdunisType
    {
        /// <summary>
        /// Semester is a parent element and is only used for time periods
        /// A time period of type semester is a container for the other time period types
        /// A semester can only contain one of each other time period type
        /// </summary>
        Semester = 1,

        /// <summary>
        /// An element of type timetable only contains information about the lecture time
        /// </summary>
        Timetable = 2,

        /// <summary>
        /// An element of type ExamPreparation only contains information about the exam preparation time
        /// </summary>
        ExamPreparation = 3,

        /// <summary>
        /// An element of type exam only contains information about the exam time
        /// </summary>
        Exam = 4
    }
}
