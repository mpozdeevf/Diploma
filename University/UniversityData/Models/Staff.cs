using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Departments = new HashSet<Department>();
            InstitutesDirector = new HashSet<Institute>();
            InstitutesDirectorDeputy = new HashSet<Institute>();
            LecturersDepartments = new HashSet<LecturerDepartment>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Institute> InstitutesDirector { get; set; }
        public virtual ICollection<Institute> InstitutesDirectorDeputy { get; set; }
        public virtual ICollection<LecturerDepartment> LecturersDepartments { get; set; }
    }
}
