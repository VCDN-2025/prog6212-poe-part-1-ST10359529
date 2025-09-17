using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    
    using Microsoft.AspNetCore.Mvc;

    namespace CMCS.Controllers
    {
        public class CoordinatorController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult PendingClaims()
            {
                return View();
            }

            [HttpPost]
            public IActionResult ApproveClaim(int id)
            {
                
                return RedirectToAction("PendingClaims");
            }

            [HttpPost]
            public IActionResult RejectClaim(int id)
            {
               
                return RedirectToAction("PendingClaims");
            }
        }
    }
}
