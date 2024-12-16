using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Subsips_2.Areas.CPanel.Controllers;

public class UserController : Controller
{

    private readonly SignInManager<IdentityUser> _signInManager;
    public UserController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Order");
    }
}
