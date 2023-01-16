using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var login = await _service.LoginAsync(loginUser);
            if (login.Message == "NotFound")
            {
                login.Message = "The username or password is incorrect";
                return StatusCode(404, login);
            }
            return Ok(login);
        }
    }
}
