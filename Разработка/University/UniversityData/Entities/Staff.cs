using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            News = new HashSet<News>();
            NewsReceivers = new HashSet<NewsReceiver>();
            StaffDepartments = new HashSet<StaffDepartment>();
            SubSubjects = new HashSet<SubSubject>();
            Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual Department Departments { get; set; }
        public virtual Institute InstitutesDirector { get; set; }
        public virtual Institute InstitutesDirectorDeputy { get; set; }
        public virtual StaffAuthData StaffAuthData { get; set; }
        public virtual StaffRequisite StaffRequisites { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<NewsReceiver> NewsReceivers { get; set; }
        public virtual ICollection<StaffDepartment> StaffDepartments { get; set; }
        public virtual ICollection<SubSubject> SubSubjects { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
