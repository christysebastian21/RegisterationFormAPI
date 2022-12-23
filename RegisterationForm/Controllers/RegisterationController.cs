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

        [HttpPost("CreateUser")]
        public string createUser([FromBody] Registerations userDetails)
        {
            try
            {
                _dbContext.RegisterationForms.Add(userDetails);
                _dbContext.SaveChanges();

                return "User Created SuccessFully";
            }
            catch (Exception ex)
            {
                return $"User creation failed: {ex.Message}";
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
    }
}
