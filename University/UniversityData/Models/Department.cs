using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Department
    {
        public Department()
        {
            Groups = new HashSet<Group>();
            LecturersDepartments = new HashSet<LecturerDepartment>();
        }

        public int Id { get; set; }
        public int? InstituteId { get; set; }
        public string Name { get; set; }
        public int? HeadId { get; set; }

        public virtual Staff Head { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<LecturerDepartment> LecturersDepartments { get; set; }
    }
}
