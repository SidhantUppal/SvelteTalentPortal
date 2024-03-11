using Microsoft.AspNetCore.Mvc;
using TalentPortal.Helpers;

namespace TalentPortal.Controllers
{
    public class BaseController : Controller
    {
        public int UserId { get; set; }
    }

}
