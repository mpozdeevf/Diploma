using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Student
    {
        public Student()
        {
            Groups = new HashSet<Group>();
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual StudentRequisites StudentRequisites { get; set; }
        public virtual StudentAuthData StudentsAuthData { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
