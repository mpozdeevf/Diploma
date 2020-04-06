using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityData.Models;
using UniversityWebApi.Data;

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

        [HttpGet]
        [Route("staff")]
        public IEnumerable<Staff> GetStaff() => _context.Staff;

        [HttpPost]
        [Route("staff")]
        public ActionResult<Staff> CreateStaff(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }

            _context.Staff.Add(staff);
            _context.SaveChanges();

            return Ok(staff);
        }

        [HttpDelete("id")]
        [Route("staff")]
        public ActionResult<Staff> DeleteStaff(int id)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            _context.SaveChanges();

            return Ok(staff);
        }

        [HttpPut]
        [Route("staff")]
        public ActionResult<Staff> PutStaff(Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }

            if (!_context.Staff.Any(s => s.Id == staff.Id))
            {
                return NotFound();
            }

            _context.Update(staff);
            _context.SaveChanges();

            return Ok(staff);
        }

        [HttpGet]
        [Route("institutes")]
        public IEnumerable<Institute> GetInstitutes() => _context.Institutes;

        [HttpPost]
        [Route("institutes")]
        public ActionResult<Institute> CreateInstitute(Institute institute)
        {
            if (institute == null)
            {
                return BadRequest();
            }

            _context.Institutes.Add(institute);
            _context.SaveChanges();

            return Ok(institute);
        }

        [HttpDelete("id")]
        [Route("institutes")]
        public ActionResult<Institute> DeleteInstitute(int id)
        {
            var institute = _context.Institutes.FirstOrDefault(i => i.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            _context.Institutes.Remove(institute);
            _context.SaveChanges();

            return Ok(institute);
        }

        [HttpPut]
        [Route("institutes")]
        public ActionResult<Institute> PutInstitute(Institute institute)
        {
            if (institute == null)
            {
                return BadRequest();
            }

            if (!_context.Institutes.Any(i => i.Id == institute.Id))
            {
                return NotFound();
            }

            _context.Update(institute);
            _context.SaveChanges();

            return Ok(institute);
        }

        [HttpGet]
        [Route("departments")]
        public IEnumerable<Department> GetDepartments() => _context.Departments;

        [HttpPost]
        [Route("departments")]
        public ActionResult<Department> CreateDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            _context.Departments.Add(department);
            _context.SaveChanges();

            return Ok(department);
        }

        [HttpDelete("id")]
        [Route("departments")]
        public ActionResult<Department> DeleteDepartment(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return Ok(department);
        }

        [HttpPut]
        [Route("departments")]
        public ActionResult<Department> PutDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            if (!_context.Departments.Any(d => d.Id == department.Id))
            {
                return NotFound();
            }

            _context.Update(department);
            _context.SaveChanges();

            return Ok(department);
        }

        [HttpGet]
        [Route("lecturersdepartments")]
        public IEnumerable<LecturerDepartment> GetLecturersDepartments() => _context.LecturersDepartments;

        [HttpPost]
        [Route("lecturersdepartments")]
        public ActionResult<LecturerDepartment> CreateLecturerDepartment(LecturerDepartment lecturerDepartment)
        {
            if (lecturerDepartment == null)
            {
                return BadRequest();
            }

            _context.LecturersDepartments.Add(lecturerDepartment);
            _context.SaveChanges();

            return Ok(lecturerDepartment);
        }

        [HttpDelete]
        [Route("lecturersdepartments")]
        public ActionResult<LecturerDepartment> DeleteLecturerDepartment(LecturerDepartment lecturerDepartment)
        {
            if (lecturerDepartment == null)
            {
                return BadRequest();
            }

            _context.LecturersDepartments.Remove(lecturerDepartment);
            _context.SaveChanges();

            return Ok(lecturerDepartment);
        }

        [HttpPut]
        [Route("lecturersdepartments")]
        public ActionResult<LecturerDepartment> PutLecturerDepartment(LecturerDepartment lecturerDepartment)
        {
            if (lecturerDepartment == null)
            {
                return BadRequest();
            }

            if (!_context.LecturersDepartments.Any(ld =>
                ld.LecturerId == lecturerDepartment.LecturerId && ld.DepartmentId == lecturerDepartment.DepartmentId))
            {
                return NotFound();
            }

            _context.Update(lecturerDepartment);
            _context.SaveChanges();

            return Ok(lecturerDepartment);
        }

        [HttpGet]
        [Route("groups")]
        public IEnumerable<Group> GetGroups() => _context.Groups;

        [HttpPost]
        [Route("groups")]
        public ActionResult<Group> CreateStaff(Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            _context.Groups.Add(group);
            _context.SaveChanges();

            return Ok(group);
        }

        [HttpDelete("id")]
        [Route("groups")]
        public ActionResult<Group> DeleteGroup(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return Ok(group);
        }

        [HttpPut]
        [Route("groups")]
        public ActionResult<Group> PutGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            if (!_context.Groups.Any(g => g.Id == group.Id))
            {
                return NotFound();
            }

            _context.Update(group);
            _context.SaveChanges();

            return Ok(group);
        }

        [HttpGet]
        [Route("students")]
        public IEnumerable<Student> GetStudents() => _context.Students;

        [HttpPost]
        [Route("students")]
        public ActionResult<Student> CreateStudent(Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }

        [HttpDelete("id")]
        [Route("students")]
        public ActionResult<Student> DeleteStudent(string id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok(student);
        }

        [HttpPut]
        [Route("students")]
        public ActionResult<Student> PutStudent(Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            if (!_context.Students.Any(s => s.StudentNumber == student.StudentNumber))
            {
                return NotFound();
            }

            _context.Update(student);
            _context.SaveChanges();

            return Ok(student);
        }
    }
}