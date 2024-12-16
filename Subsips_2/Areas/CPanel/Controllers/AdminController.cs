using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
[Authorize]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult CafeUsers()
    {
        var model = _userManager.Users.Select(x => new CafeUsersModelView 
        { 
            UserName = x.UserName,
            IsEmailConfirmed = x.EmailConfirmed,
            Id = x.Id

        }).ToList();
        return View(model);
    }

    public async Task<IActionResult> UserConfirmed(string id)
    {
        var model = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        if (model is null)
            return RedirectToAction("CafeUsers");

        var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(model);
        await _userManager.ConfirmEmailAsync(model, tokenEmail);

        return RedirectToAction("CafeUsers");
    }
}
