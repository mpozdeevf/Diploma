using System.Collections.Generic;

namespace UniversityWebApi.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }
}
