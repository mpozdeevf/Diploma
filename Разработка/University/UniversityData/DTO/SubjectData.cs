using System.Collections.Generic;

namespace UniversityData.DTO
{
    public class SubjectData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int LecturerId { get; set; }
        public int Semester { get; set; }

        public List<SubSubjectData> SubSubjectsData { get; set; }
    }
}