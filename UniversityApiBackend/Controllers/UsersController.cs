using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Authorize(Roles = ("Admin"))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResDTO>>> GetUsers()
        {
            var users = await _service.GetUsersAsync();

            if (users.Message == "NotFound")
            {
                users.Message = $"No User in Database";
                return StatusCode(404, users);
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResDTO>> GetUser(int id)
        {
            var user = await _service.GetUserByIdAsync(id);

            if (user.Success == false & user.Message == "NotFound")
            {
                user.Message = $"No User found for id: {id}";
                return StatusCode(404, user);
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResDTO>> PutUser(int id, UserReqDTO User)
        {
            var userModified = await _service.PutUserAsync(id, User);
            if (userModified.Success == false & userModified.Message == "NotFound")
            {
                userModified.Message = $"No User found for id: {id}";
                return StatusCode(404, userModified);
            }
            return Ok(userModified);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserResDTO>> PostUser(UserReqDTO UserDto)
        {
            var user = await _service.PostUserAsync(UserDto);
            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _service.DeleteUserAsync(id);
            if (user.Success == false & user.Message == "NotFound")
            {
                user.Message = $"No User found for id: {id}";
                return StatusCode(404, user);
            }
            return StatusCode(StatusCodes.Status200OK, user);
        }

    }
}
