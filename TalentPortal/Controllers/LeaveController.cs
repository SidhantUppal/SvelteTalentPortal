using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("[controller]")]
    public class LeaveController : BaseController
    {
        private readonly ILeaveService leaveService;
        public LeaveController(ILeaveService leaveService)
        {
            this.leaveService = leaveService;
        }
        [HttpGet]
        public async Task<IActionResult> GetLeaves()
        {
            var result = await leaveService.GetByUser(UserId);
            ResponseModel<IEnumerable<LeaveSearch>> response = new ResponseModel<IEnumerable<LeaveSearch>>
            {
                Status = result.Any(),
                Data = result,
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> GetSentLeaves()
        {
            IEnumerable<LeaveSearch> result = await leaveService.GetSentLeaves(UserId);
            ResponseModel<IEnumerable<LeaveSearch>> response = new ResponseModel<IEnumerable<LeaveSearch>>
            {
                Status = result.Any(),
                Data = result,
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddLeave(LeaveDto dto)
        {
            dto.UserId = UserId;
            var result = await leaveService.Add(dto);
            ResponseModel<int> response = new ResponseModel<int>
            {
                Status = result > 0,
                Data = result
            };
            return Ok(response);
        }
    }
}
