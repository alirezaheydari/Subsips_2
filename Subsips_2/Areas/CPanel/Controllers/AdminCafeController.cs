using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
[Authorize]
public class AdminCafeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
