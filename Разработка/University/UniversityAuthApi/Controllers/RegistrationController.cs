using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using UniversityData;
using UniversityData.Models;

namespace UniversityAuthApi.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly UniversityContext _context;

        public RegistrationController(UniversityContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register-student")]
        public IActionResult RegisterStudent(string studentNumber, string username, string password)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
            if (student != null)
            {
                var salt = GenerateSalt();
                var hashedPassword = GetHashedPassword(salt, password);
                _context.AuthUserStudents.Add(new AuthUserStudent
                {
                    Id = student.Id,
                    Username = username,
                    Password = hashedPassword,
                    Salt = Convert.ToBase64String(salt),
                    Student = student
                });
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string GetHashedPassword(byte[] salt, string password) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
    }
}