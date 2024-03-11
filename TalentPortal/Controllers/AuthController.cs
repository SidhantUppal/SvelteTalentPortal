using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Helpers;
using TalentPortal.BAL.Interfaces;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        public IUserService _userService { get; set; }
        public AuthController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _userService.GetUser(loginDto.Username);
            if (res == null)
            {
                return Unauthorized();
            }
            else
            {// check for salt verification
                var isVerified = SecretHasher.Verify(loginDto.Password, res.Password);
                if (isVerified)
                {
                    string token = new JwtUtils().GenerateJwtToken(res.Id);
                    return Ok(new
                    {
                        UserId = res.Id,
                        Username = res.UserName,
                        AccessToken = token,
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var result = await _userService.CreateUser(user);
            if (result > 0)
            {
                var res = new
                {
                    Status = true,
                    Data = result
                };
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
        [JwtAuthorize]
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser()
        {
            UserDto result = await _userService.GetUser(UserId);
            ResponseModel<UserDto> response = new ResponseModel<UserDto>
            {
                Status = result != null,
                Data = result
            };
            return Ok(response);
        }
    }
}
