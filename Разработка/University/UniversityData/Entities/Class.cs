using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class Class
    {
        public Class()
        {
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
