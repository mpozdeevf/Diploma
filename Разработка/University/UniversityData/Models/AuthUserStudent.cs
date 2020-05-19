using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityData.Models
{
    public partial class AuthUserStudent
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }

        public virtual Student Student { get; set; }
    }
}   
