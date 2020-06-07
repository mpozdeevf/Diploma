using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Department
    {
        public Department()
        {
            Groups = new HashSet<Group>();
            NewsReceivers = new HashSet<NewsReceiver>();
            StaffDepartments = new HashSet<StaffDepartment>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? DirectorId { get; set; }

        public virtual Staff Director { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<NewsReceiver> NewsReceivers { get; set; }
        public virtual ICollection<StaffDepartment> StaffDepartments { get; set; }
    }
}
