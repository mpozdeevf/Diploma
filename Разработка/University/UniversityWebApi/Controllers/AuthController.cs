using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniversityData;
using UniversityData.Models;

namespace UniversityWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UniversityContext _context;
        private readonly IConfiguration _config;

        public AuthController(UniversityContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("authorize-student")]
        public IActionResult AuthorizeStudent(string username, string password)
        {
            var user = _context.AuthUserStudents.FirstOrDefault(s => s.Username == username);
            if (user != null && IsCorrectPassword(user, password))
            {
                var encodedJwt = GenerateAccessToken(user);

                user.RefreshToken = GenerateRefreshToken();
                _context.SaveChanges();

                return Ok(new { access_token = encodedJwt, user_id = user.Id, refresh_token = user.RefreshToken });
            }
            else
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
        }

        [HttpPost]
        [Route("refresh-access-token")]
        public IActionResult RefreshAccessToken(string accessToken, string refreshToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = false,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                return BadRequest(new { errorText = "Invalid access token" });
            }

            var username = principal.FindFirst(ClaimTypes.Name)?.Value;
            var user = _context.AuthUserStudents.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return BadRequest(new { errorText = "Invalid access token" });
            }

            if (!user.RefreshToken.Equals(refreshToken, StringComparison.InvariantCulture))
            {
                return BadRequest(new { errorText = "Invalid refresh token" });
            }

            var encodedJwt = GenerateAccessToken(user);

            user.RefreshToken = GenerateRefreshToken();
            _context.SaveChanges();

            return Ok(new { access_token = encodedJwt, user_id = user.Id, refresh_token = user.RefreshToken });
        }

        private bool IsCorrectPassword(AuthUserStudent user, string password)
        {
            var salt = Convert.FromBase64String(user.Salt);
            var hashedPassword = GetHashedPassword(salt, password);
            return hashedPassword == user.Password;
        }

        private string GetHashedPassword(byte[] salt, string password) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

        private string GenerateAccessToken(AuthUserStudent user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username)
                };
            var jwt = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}