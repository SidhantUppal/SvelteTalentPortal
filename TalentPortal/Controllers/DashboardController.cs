using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL.Models;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        private readonly IDsrService _dsrService;
        public DashboardController(IDashboardService dashboardService, IDsrService dsrService)
        {
            _dashboardService = dashboardService;
            _dsrService = dsrService;
        }
        [HttpGet]
        [Route("attendance/today")]
        public async Task<IActionResult> AttendanceToday()
        {
            try
            {
                AttendanceLog result = await _dashboardService.GetTodayAttendance(UserId);
                ResponseModel<AttendanceLog> response = new ResponseModel<AttendanceLog>
                {
                    Status = result != null,
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("attendance")]
        public async Task<IActionResult> Attendance(int id, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var data = await _dashboardService.GetAttendanceLogAsync(id);
                var res = new
                {
                    Status = data.Any(),
                    Data = data,
                };
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("timein")]
        public async Task<IActionResult> TimeIn()
        {
            try
            {
                int result = await _dashboardService.RecordTime(UserId);
                ResponseModel<int> response = new ResponseModel<int>
                {
                    Status = result > 0,
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpPost]
        [Route("timeout")]
        public async Task<IActionResult> TimeOut()
        {
            try
            {
                bool isDsrDone = await _dsrService.IsDsrCompleted(UserId);
                if (!isDsrDone)
                {
                    ResponseModel<bool> res = new ResponseModel<bool>
                    {
                        Status = isDsrDone,
                        Data = isDsrDone
                    };
                    return Ok(res);

                }
                int result = await _dashboardService.RecordTime(UserId, true);
                ResponseModel<int> response = new ResponseModel<int>
                {
                    Status = result > 0,
                    Data = result
                };
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
