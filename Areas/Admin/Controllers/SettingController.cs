using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudioWebApp.DAL;
using StudioWebApp.Models;
using StudioWebApp.ViewModels.SettingVM;
using System.Data;

namespace StudioWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{
    private readonly StudioDbContext _studioDbContext;
    public SettingController(StudioDbContext studioDbContext)
    {
        _studioDbContext= studioDbContext;
    }
    public IActionResult Index()
    {
        List<Setting> settings= _studioDbContext.Settings.ToList();
        return View(settings);
    }
    public async Task<IActionResult> Update(int id)
    {
        Setting? setting = await _studioDbContext.Settings.FindAsync(id);
        if (setting == null) return NotFound();

        UpdateSettingVM updateSettingVM = new UpdateSettingVM()
        {
            Value = setting.Value,
        };
        return View(updateSettingVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateSettingVM updateSettingVM)
    {
        Setting? setting = await _studioDbContext.Settings.FindAsync(id);
        if (setting == null) return NotFound();

        if (!ModelState.IsValid) return View();

        setting.Value = updateSettingVM.Value;

        _studioDbContext.Settings.Update(setting);
        await _studioDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
