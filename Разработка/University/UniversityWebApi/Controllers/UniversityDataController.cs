using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityData;
using UniversityData.Entities;

namespace UniversityWebApi.Controllers
{
    [Route("api/university")]
    [Authorize]
    public class UniversityDataController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public UniversityDataController(UniversityDbContext context)
        {
            _context = context;
        }

        [HttpGet("id")]
        [Route("user-student-requisites")]
        public ActionResult<StudentRequisites> GetUserStudentRequisites(int id)
        {
            var studentRequisite = _context.StudentRequisites.FirstOrDefault(sr => sr.StudentId == id);
            if (studentRequisite == null)
            {
                return NotFound();
            }

            return Ok(studentRequisite);
        }

        [HttpGet("groupId")]
        [Route("student-group")]
        public IEnumerable<Group> GetStudentGroup(int groupId) => 
            _context.Groups.Where(gs => gs.Id == groupId);

        [HttpGet("groupId")]
        [Route("group-subjects")]
        public IEnumerable<GroupSubject> GetGroupSubjects(int groupId) =>
            _context.GroupsSubjects.Where(gs => gs.GroupId == groupId);
    }
}