using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class GroupSubjectLink
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int SemesterNumber { get; set; }

        public virtual StudentGroup Group { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
