using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace CMCS.Controllers
    {
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }
    }
}
