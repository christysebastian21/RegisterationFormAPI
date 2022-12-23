using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterationForm.Models;

namespace RegisterationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly RegisterFormDBContext _dbContext;
        public RegisterationController(RegisterFormDBContext registerFormDBContext)
        {
            _dbContext = registerFormDBContext;
        }

        [HttpPost("Register")]
        public ActionResult<Registerations> Register([FromBody] Registerations userDetails)
        {
            try
            {
                userDetails.Password = BCrypt.Net.BCrypt.HashPassword(userDetails.Password);
                _dbContext.RegisterationForms.Add(userDetails);
                _dbContext.SaveChanges();

                return userDetails;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRegisterationDetailsUsingRegisterationID/{registerationID}")]
        public ActionResult<Registerations> GetUserDetailsUsingRegisterationID(int registerationID)
        {
            try
            {
                var registerationDetails = _dbContext.RegisterationForms.Find(registerationID);

                return Ok(registerationDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login(Registerations loginDetails)
        {
            try
            {
                var message = string.Empty;
                var user = _dbContext.RegisterationForms.SingleOrDefault(x => x.Username == loginDetails.Username);

                if (user != null)
                {
                    bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDetails.Password, user.Password);

                    if (isValidPassword)
                    {
                        message = "Login success";
                    }
                    else
                    {
                        message = "Login failed";
                    }
                }
                else
                {
                    message = "Login failed";
                }

                return message;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
