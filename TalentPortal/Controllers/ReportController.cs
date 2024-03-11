using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        [HttpGet]
        [Route("received")]
        public async Task<IActionResult> GetReceivedReports()
        {
            List<ReportSearch> result = await reportService.GetReceivedReport(UserId);
            ResponseModel<List<ReportSearch>> response = new ResponseModel<List<ReportSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> GetSentReports()
        {

            List<ReportSearch> result = await reportService.GetByUser(UserId);
            ResponseModel<List<ReportSearch>> response = new ResponseModel<List<ReportSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveReport(ReportDto report)
        {
            report.UserId = UserId;
            var result = await reportService.Add(report);
            ResponseModel<int> response = new ResponseModel<int>
            {
                Status = result > 0,
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return BadRequest();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateReport(int id)
        {
            return BadRequest(ModelState);

        }
    }
}
