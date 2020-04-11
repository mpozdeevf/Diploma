using System;
using System.Collections.Generic;

namespace UniversityData
{
    public partial class StudentRequisite
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string EMail { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PhotoLink { get; set; }

        public virtual StudentGroup Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
