using System.Collections.Generic;

namespace UniversityWebApi.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Departments = new HashSet<Departments>();
            InstitutesDirector = new HashSet<Institutes>();
            InstitutesDirectorDeputy = new HashSet<Institutes>();
            LecturersDepartments = new HashSet<LecturersDepartments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<Institutes> InstitutesDirector { get; set; }
        public virtual ICollection<Institutes> InstitutesDirectorDeputy { get; set; }
        public virtual ICollection<LecturersDepartments> LecturersDepartments { get; set; }
    }
}
