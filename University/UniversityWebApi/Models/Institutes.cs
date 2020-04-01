using System.Collections.Generic;

namespace UniversityWebApi.Models
{
    public partial class Institutes
    {
        public Institutes()
        {
            Departments = new HashSet<Departments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DirectorId { get; set; }
        public int? DirectorDeputyId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Staff DirectorDeputy { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
    }
}
