namespace UniversityData.Models
{
    public partial class GroupStudentV
    {
        public int? GroupId { get; set; }
        public string StudentName { get; set; }
        public long? SerialNumber { get; set; }
        public int? StudentId { get; set; }
        public bool? IsHeadOfGroup { get; set; }
    }
}
