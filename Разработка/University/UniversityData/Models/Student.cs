using System.Collections.Generic;

namespace UniversityData.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentGroup = new HashSet<StudentGroup>();
        }

        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual StudentRequisite StudentRequisite { get; set; }
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
    }
}
