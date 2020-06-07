using System;

namespace UniversityData.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int? GroupId { get; set; }
        public int SubjectId { get; set; }
        public int? SubSubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool WeekLine { get; set; }
        public string Location { get; set; }

        public virtual Class Class { get; set; }
        public virtual Group Group { get; set; }
        public virtual SubSubject SubSubject { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
