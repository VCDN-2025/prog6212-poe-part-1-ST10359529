using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    namespace CMCS.Controllers
    {
        public class LecturerController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult SubmitClaim()
            {
                return View();
            }

            [HttpPost]
            public IActionResult SubmitClaim(Claim model)
            {
                
                return View(model);
            }
        }
    }
}
