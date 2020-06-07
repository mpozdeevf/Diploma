namespace UniversityData.Entities
{
    public partial class StudentRequisites
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string EMail { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }

        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
