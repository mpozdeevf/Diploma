using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityData;

namespace UniversityWebApi.Controllers
{
    [Route("api/university")]
    public class UniversityDataController : ControllerBase
    {
        private readonly UniversityContext _context;

        public UniversityDataController(UniversityContext context)
        {
            _context = context;
        }

        [HttpGet("id")]
        [Route("user-student-requisites")]
        public ActionResult<StudentRequisite> GetUserStudentRequisites(int id)
        {
            var studentRequisite = _context.StudentRequisite.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            return Ok(studentRequisite);
        }

        [HttpGet("groupId")]
        [Route("student-group")]
        public IEnumerable<GroupStudentV> GetStudentGroup(int groupId) => _context.GroupStudentV;
    }
}