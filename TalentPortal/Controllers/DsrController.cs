using Microsoft.AspNetCore.Mvc;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthorize]
    public class DsrController : BaseController
    {
        private readonly IDsrService dsrService;
        public DsrController(IDsrService dsrService)
        {
            this.dsrService = dsrService;
        }
        int[] numbers = { 1, 2, 3, 4, 5 };
        [HttpGet]
        [Route("received")]
        public async Task<IActionResult> GetReceivedDsrs()
        {
            var result = await dsrService.ReceivedDsrs(UserId);
            ResponseModel<IEnumerable<DsrSearch>> response = new ResponseModel<IEnumerable<DsrSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> GetSentDsrs()
        {
            var result = await dsrService.SentDsrs(UserId);
            ResponseModel<IEnumerable<DsrSearch>> response = new ResponseModel<IEnumerable<DsrSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("log/{id}")]
        public async Task<IActionResult> GetDsrByLog(int id)
        {
            var result = await dsrService.GetDsr(id);
            ResponseModel<IEnumerable<DsrSearch>> response = new ResponseModel<IEnumerable<DsrSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("today")]
        public async Task<IActionResult> GetTodayDsrs()
        {
            var result = await dsrService.GetDsr(true, UserId);
            ResponseModel<IEnumerable<DsrSearch>> response = new ResponseModel<IEnumerable<DsrSearch>>
            {
                Status = result.Any(),
                Data = result
            };
            return Ok(response);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> SaveDsr(DsrDto dsr)
        {
            dsr.Name = dsr.Name;
            dsr.DateTime = DateTime.Now;
            dsr.Description = dsr.Description;
            dsr.Status = "Pending";
            dsr.FromTime = dsr.FromTime;
            dsr.ToTime = dsr.ToTime;
            dsr.UserId = UserId;
            if (dsr.Id > 0)
            {
                var uresult = await dsrService.UpdateDsr(dsr);
                ResponseModel<int> uResponse = new ResponseModel<int>
                {
                    Status = uresult > 0,
                    Data = uresult
                };
                return Ok(uResponse);
            }
            var result = await dsrService.AddDsr(dsr);
            ResponseModel<int> response = new ResponseModel<int>
            {
                Status = result > 0,
                Data = result
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDsr(int id)
        {
            return Ok();
            //id += 8;
            //return GetTodayDsrs(id);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateDsr(int id)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, numbers.Length);

            // Pick the value at the random index
            int randomValue = numbers[randomIndex];
            DsrSearch dsr = new DsrSearch
            {
                Id = randomValue, // Assuming IDs start from 1
                DateTime = DateTime.Now.AddDays(-random.Next(1, 365)), // Random date within the last year
                ProjectName = "Project " + randomValue, // Unique project name
                ProjectId = randomValue, // Random project ID
                Description = "This is Project Description",
                Status = "Pending",
                FromTime = DateTime.Now.AddHours(-2),
                ToTime = DateTime.Now.AddHours(-1),
            };
            return Ok(dsr);
        }
        [HttpPost]
        [Route("final")]
        public async Task<IActionResult> SubmitDsr(DsrSubmitDto dto)
        {
            var result = await dsrService.SubmitDsr(dto, UserId);
            ResponseModel<bool> response = new ResponseModel<bool>
            {
                Status = result,
                Data = result
            };
            return Ok(response);
        }

    }
}
