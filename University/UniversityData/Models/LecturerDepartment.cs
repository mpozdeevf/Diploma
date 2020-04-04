namespace UniversityData.Models
{
    public partial class LecturerDepartment
    {
        public int DepartmentId { get; set; }
        public int LecturerId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Staff Lecturer { get; set; }
    }
}
