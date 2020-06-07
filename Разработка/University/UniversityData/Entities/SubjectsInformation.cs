using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class SubjectInformation
    {
        public SubjectInformation()
        {
            Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
