using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL.Models;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("[controller]")]
    public class CommonController : BaseController
    {
        private readonly ICommonService commonService;
        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }
        [HttpGet]
        [Route("project")]
        public async Task<IActionResult> GetProjects()
        {
            var result = await commonService.GetAllProjects();
            ResponseModel<IEnumerable<Project>> response = new ResponseModel<IEnumerable<Project>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUsers()
        {
            List<UserDto> result = await commonService.GetAllUsers();
            result = result.Where(x => x.Id != UserId).ToList();
            ResponseModel<IEnumerable<UserDto>> response = new ResponseModel<IEnumerable<UserDto>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("leave-type")]
        public async Task<IActionResult> GetLeaveTypes()
        {
            List<LeaveType> result = await commonService.GetLeaveTypes();
            ResponseModel<IEnumerable<LeaveType>> response = new ResponseModel<IEnumerable<LeaveType>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
    }
}
