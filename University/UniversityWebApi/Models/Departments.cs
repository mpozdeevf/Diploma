using System.Collections.Generic;

namespace UniversityWebApi.Models
{
    public partial class Departments
    {
        public Departments()
        {
            Groups = new HashSet<Groups>();
            LecturersDepartments = new HashSet<LecturersDepartments>();
        }

        public int Id { get; set; }
        public int? InstituteId { get; set; }
        public string Name { get; set; }
        public int? HeadId { get; set; }

        public virtual Staff Head { get; set; }
        public virtual Institutes Institute { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<LecturersDepartments> LecturersDepartments { get; set; }
    }
}
