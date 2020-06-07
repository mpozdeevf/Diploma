using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Group
    {
        public Group()
        {
            GroupsSubjects = new HashSet<GroupSubject>();
            NewsReceivers = new HashSet<NewsReceiver>();
            Schedule = new HashSet<Schedule>();
            StudentRequisites = new HashSet<StudentRequisites>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int HeadId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Student Head { get; set; }
        public virtual ICollection<GroupSubject> GroupsSubjects { get; set; }
        public virtual ICollection<NewsReceiver> NewsReceivers { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<StudentRequisites> StudentRequisites { get; set; }
    }
}
