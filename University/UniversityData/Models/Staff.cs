using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Staff
    {
        public Staff()
        {
            StaffDepartmentLink = new HashSet<StaffDepartmentLink>();
            SubSubject = new HashSet<SubSubject>();
            Subject = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual Department Department { get; set; }
        public virtual Institute InstituteDirector { get; set; }
        public virtual Institute InstituteDirectorDeputy { get; set; }
        public virtual ICollection<StaffDepartmentLink> StaffDepartmentLink { get; set; }
        public virtual ICollection<SubSubject> SubSubject { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
