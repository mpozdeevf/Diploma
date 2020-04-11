using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class Institute
    {
        public Institute()
        {
            Department = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public int DirectorId { get; set; }
        public int DirectorDeputyId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Staff DirectorDeputy { get; set; }
        public virtual ICollection<Department> Department { get; set; }
    }
}
