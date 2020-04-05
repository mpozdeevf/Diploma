using System.ComponentModel;
using System.Runtime.CompilerServices;
using UniversityAdministrator.Annotations;

namespace UniversityAdministrator.Models
{
    public class StudentModel
    {
        public string Institute { get; set; }
        public string Department { get; set; }
        public string Group { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string StudentNumber { get; set; }
    }
}