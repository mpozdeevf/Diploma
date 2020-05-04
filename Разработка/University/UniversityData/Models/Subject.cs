using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Subject
    {
        public Subject()
        {
            GroupSubjectLink = new HashSet<GroupSubjectLink>();
            SubSubject = new HashSet<SubSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public int LecturerId { get; set; }

        public virtual Staff Lecturer { get; set; }
        public virtual ICollection<GroupSubjectLink> GroupSubjectLink { get; set; }
        public virtual ICollection<SubSubject> SubSubject { get; set; }
    }
}
