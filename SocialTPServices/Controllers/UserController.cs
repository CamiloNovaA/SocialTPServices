using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialTPServices.Data;
using SocialTPServices.Models;

namespace SocialTPServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SocialTPDBContext _context;

        public UserController(SocialTPDBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        // GET: api/User/profileInfo/5
        [HttpGet("profileInfo/{id}")]
        public async Task<IActionResult> GetProfileInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.IdUser)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.Entry(user).Property(x => x.RegisterDate).IsModified = false;
                _context.Entry(user).Property(x => x.ProfilePhoto).IsModified = false;

                if (!user.Password.Length.Equals(0))
                {
                    user.Password = Utilities.Encrypt.EncodePassword(user.Password);
                }
                else
                {
                    _context.Entry(user).Property(x => x.Password).IsModified = false;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = _context.User.FromSql($"ValidateEmail {user.Email}").ToList();
            var userName = _context.User.FromSql($"ValidateUserName {user.UserName}").ToList();

            if (email.Count > 0)
                return BadRequest("Correo registrado");
            else if (userName.Count > 0)
                return BadRequest("Usuario registrado");

            user.RegisterDate = DateTime.Now;
            user.Password = Utilities.Encrypt.EncodePassword(user.Password);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("userSecretKeyForAuthenticationSocialTP"));
            var userCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: user.IdUser.ToString(),
                audience: user.UserName,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: userCredential
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }

        // POST: api/User
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getUser = _context.User.FromSql($"GetUserName {user.UserName}").FirstOrDefault();

            if (getUser == null)
            {
                return BadRequest("Usuario no existe");
            }

            user.Password = Utilities.Encrypt.EncodePassword(user.Password);

            var userLog = _context.User.FromSql($"UserLogin {user.UserName}, {user.Password}").FirstOrDefault();

            if (userLog == null)
            {
                return BadRequest("Usuario o contraseña incorrecta");
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("userSecretKeyForAuthenticationSocialTP"));
            var userCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: getUser.IdUser.ToString(),
                audience: user.UserName,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: userCredential
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.IdUser == id);
        }
    }
}