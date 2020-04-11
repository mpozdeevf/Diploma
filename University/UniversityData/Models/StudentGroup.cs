using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            GroupSubjectLink = new HashSet<GroupSubjectLink>();
            StudentRequisite = new HashSet<StudentRequisite>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int HeadId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Student Head { get; set; }
        public virtual ICollection<GroupSubjectLink> GroupSubjectLink { get; set; }
        public virtual ICollection<StudentRequisite> StudentRequisite { get; set; }
    }
}
