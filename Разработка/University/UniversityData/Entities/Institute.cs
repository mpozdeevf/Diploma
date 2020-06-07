using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Institute
    {
        public Institute()
        {
            Departments = new HashSet<Department>();
            NewsReceivers = new HashSet<NewsReceiver>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int DirectorId { get; set; }
        public int DirectorDeputyId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Staff DirectorDeputy { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<NewsReceiver> NewsReceivers { get; set; }
    }
}
