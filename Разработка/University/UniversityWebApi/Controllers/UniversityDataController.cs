using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityData;
using UniversityData.Models;

namespace UniversityWebApi.Controllers
{
    [Route("api/university")]
    [Authorize]
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
        public IEnumerable<GroupStudentV> GetStudentGroup(int groupId) => 
            _context.GroupStudents.Where(gs => gs.GroupId == groupId);

        [HttpGet("groupId")]
        [Route("group-subjects")]
        public IEnumerable<GroupSubjectV> GetGroupSubjects(int groupId) =>
            _context.GroupSubjects.Where(gs => gs.GroupId == groupId);
    }
}