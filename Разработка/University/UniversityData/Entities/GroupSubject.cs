namespace UniversityData.Entities
{
    public partial class GroupSubject
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int Semester { get; set; }

        public virtual Group Group { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
