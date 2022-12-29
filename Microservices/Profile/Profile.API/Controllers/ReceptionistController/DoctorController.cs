using Microsoft.AspNetCore.Mvc;

namespace Profile.API.Controllers.ReceptionistController
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
