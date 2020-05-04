using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Department
    {
        public Department()
        {
            StaffDepartmentLink = new HashSet<StaffDepartmentLink>();
            StudentGroup = new HashSet<StudentGroup>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public int? DirectorId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<StaffDepartmentLink> StaffDepartmentLink { get; set; }
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
    }
}
