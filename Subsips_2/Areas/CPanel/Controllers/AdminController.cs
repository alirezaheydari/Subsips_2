﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.DataModel;
using Subsips_2.Areas.CPanel.Models.Admin.FormRequest;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.SubwayStation;
using System.Security.Claims;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
[Authorize]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ICafeStationRepository cafeStationRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ISubwayStationRepository subwayStationRepository;
    private readonly ICafeStationAspNetUserRepository cafeStationAspNetUserRepository;
    public AdminController(UserManager<IdentityUser> userManager, ICafeStationRepository cafeStationRepository, IHttpContextAccessor httpContextAccessor, ICafeStationAspNetUserRepository cafeStationAspNetUserRepository, ISubwayStationRepository subwayStationRepository)
    {
        _userManager = userManager;
        this.cafeStationRepository = cafeStationRepository;
        this.httpContextAccessor = httpContextAccessor;
        this.cafeStationAspNetUserRepository = cafeStationAspNetUserRepository;
        this.subwayStationRepository = subwayStationRepository;
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
    public IActionResult Stations()
    {
        var stattions = this.subwayStationRepository.GetAll();
        if (stattions.IsFailed)
            return NotFound();
        return View(new StationsModeView
        {
            Items = stattions.Result.Select(x => new StationItemModeView
            {
                Description = x.Description,
                Id = x.Id,
                IsActive = x.IsActive,
                Line = (SubwayLines)x.Line,
                Name = x.Name
            }).ToList()
        });
    }
    public async Task<IActionResult> DeleteStationsAsync(Guid id)
    {
        var stattions = await subwayStationRepository.Delete(id);
        return RedirectToAction("Stations");
    }
    public async Task<IActionResult> ActiveStation(Guid id)
    {
        var stattions = await subwayStationRepository.ChagneStatus(id);
        return RedirectToAction("Stations");
    }
    public IActionResult AddEditStations(Guid id)

    {
        if (id == Guid.Empty)
            return View();

        var stattions = this.subwayStationRepository.Find(id);
        if (stattions.IsFailed)
            return NotFound();
        return View(new AddEditStationFormRequest
        {
            Id = id,
            Name = stattions.Result.Name,
            Description = stattions.Result.Description,
            IsActive = true,
            Line = (SubwayLines)stattions.Result.Line
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddEditStations(AddEditStationFormRequest formRequest)
    {
        // tODO : should update 
        var stattions = await this.subwayStationRepository.Add(formRequest.Name, formRequest.Line, formRequest.Description);
        if (stattions.IsFailed)
            return NotFound();
        return View();
    }
    public IActionResult Edit(string id)
    {
        var model = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        if (model is null)
            return RedirectToAction("CafeUsers");
        var cafes = cafeStationRepository.GetCafeStationInfoViewModel();

        return View(new EditCafeUserModelView
        {
            Email = model.Email,
            UserId = model.Id,
            Cafes = cafes.Result
        });
    }
    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] EditCafeUserRequestModel formRequest)
    {
        var userModel = _userManager.Users.Where(x => x.Id == formRequest.UserId).FirstOrDefault();
        if (userModel is null)
            return RedirectToAction("CafeUsers");
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        await cafeStationAspNetUserRepository.Add(formRequest.CafeId, formRequest.UserId, formRequest.IsActive, formRequest.IsOwner);

        return RedirectToAction("CafeUsers");
    }
    public IActionResult Cafes()
    {
        var cafe = cafeStationRepository.GetAll();
        return View(new CafesViewModel
        {
            Items = cafe.Result.Select(x => new CafeItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                StationName = x.Station?.Name ?? "-"
            }).ToList()
        });
    }
    public IActionResult AddEditCafe(Guid id)
    {

        var stations = subwayStationRepository.GetAll();

        if (stations is null || stations.IsFailed)
            return NotFound();

        if (id == Guid.Empty)
            return View(new AddEditCafeViewModel
            {
                Stations = stations.Result.Select(x => new AddEditStationsCafeItem
                {
                    Id = x.Id,
                    DisplayName = x.Name,
                    IsSelected = false
                }).ToList()
            });

        var cafe = cafeStationRepository.Find(id);

        if (cafe is null || cafe.IsFailed)
            return NotFound();

        return View(new AddEditCafeViewModel
        {
            Id = id,
            Description = cafe.Result.Description,
            Name = cafe.Result.Name,
            PhoneNumber = cafe.Result.PhoneNumber,
            StationId = cafe.Result.StationId,
            Stations = stations.Result.Select(x => new AddEditStationsCafeItem { 
                Id = x.Id,
                DisplayName = x.Name,
                IsSelected = x.Id == cafe.Result.StationId
            }).ToList()
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddEditCafe(AddEditCafeViewModel formRequest)
    {
        if (formRequest is null)
            return NotFound();

        if (formRequest.Id == Guid.Empty)
            await cafeStationRepository.Add(formRequest.Name, formRequest.PhoneNumber, formRequest.StationId, formRequest.Description);
        else
            await cafeStationRepository.Update(formRequest.Id, formRequest.Name, formRequest.PhoneNumber, formRequest.StationId, formRequest.Description);

        return RedirectToAction("Cafes");
    }
}
