using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            GroupsSubjects = new HashSet<GroupSubject>();
            Schedule = new HashSet<Schedule>();
            SubSubjects = new HashSet<SubSubject>();
        }

        public int Id { get; set; }
        public int LecturerId { get; set; }
        public int InformationId { get; set; }

        public virtual SubjectInformation Information { get; set; }
        public virtual Staff Lecturer { get; set; }
        public virtual ICollection<GroupSubject> GroupsSubjects { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<SubSubject> SubSubjects { get; set; }
    }
}
