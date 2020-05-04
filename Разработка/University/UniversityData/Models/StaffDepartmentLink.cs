namespace UniversityData.Models
{
    public partial class StaffDepartmentLink
    {
        public int DepartmentId { get; set; }
        public int StaffId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
