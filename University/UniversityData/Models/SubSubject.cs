using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class SubSubject
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public int SubLecturerId { get; set; }

        public virtual Staff SubLecturer { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
