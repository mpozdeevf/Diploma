using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class GroupStudentV
    {
        public string GroupName { get; set; }
        public string StudentName { get; set; }
        public long? SerialNumber { get; set; }
        public int? StudentId { get; set; }
        public bool? IsHeadOfGroup { get; set; }
    }
}
