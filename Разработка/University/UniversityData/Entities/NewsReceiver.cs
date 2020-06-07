namespace UniversityData.Entities
{
    public partial class NewsReceiver
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int? GroupId { get; set; }
        public int? StaffId { get; set; }
        public int? DepartmentId { get; set; }
        public int? InstituteId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Group Group { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual News News { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
