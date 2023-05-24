using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioWebApp.DAL;
using StudioWebApp.Models;

namespace StudioWebApp.ViewComponents;

public class HeaderViewComponent:ViewComponent
{
    private readonly StudioDbContext _studioDbContext;
	public HeaderViewComponent(StudioDbContext studioDbContext)
	{
		_studioDbContext= studioDbContext;
	}
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Dictionary<string, Setting> settings = await _studioDbContext.Settings.ToDictionaryAsync(s=>s.Key);
		return View(settings);
	}
}
