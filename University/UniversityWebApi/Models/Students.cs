namespace UniversityWebApi.Models
{
    public partial class Students
    {
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int? GroupId { get; set; }

        public virtual Groups Group { get; set; }
    }
}
