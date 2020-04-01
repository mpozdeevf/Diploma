namespace UniversityWebApi.Models
{
    public partial class LecturersDepartments
    {
        public int DepartmentId { get; set; }
        public int LecturerId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual Staff Lecturer { get; set; }
    }
}
