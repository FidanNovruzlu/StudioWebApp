using Microsoft.AspNetCore.Mvc;

namespace StudioWebApp.Controllers
{
    public class AccountController:Controller
    {
       public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
