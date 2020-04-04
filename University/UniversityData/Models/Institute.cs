using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Institute
    {
        public Institute()
        {
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DirectorId { get; set; }
        public int? DirectorDeputyId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Staff DirectorDeputy { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
