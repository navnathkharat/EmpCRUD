using EmpAPI.Models;
using EmpAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class LoginController : ControllerBase
    {
       private IAuthentication _authentication;

        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult Login(string username, string password)
        {
            UserModel login = new UserModel() { UserName = username, Password = password };

            IActionResult Response = Unauthorized();

            var user = _authentication.AuthenticateUser(login);

            if (user != null)
            {
                var tokenstr = _authentication.GenerateJSONWebToken(user);

                Response = Ok(new { token = tokenstr });
            }

            return Response;
        }

        [HttpPost("Post")]
        [AllowAnonymous]
        public IActionResult RegisterUser(string username, string password)
        {
            UserModel model = new UserModel() { UserName = username, Password = password };

            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }

            string result = _authentication.RegisterUser(model);

            if (string.IsNullOrEmpty(result))
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3" };
        }
    }
}
