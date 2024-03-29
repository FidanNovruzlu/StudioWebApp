﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioWebApp.DAL;
using StudioWebApp.Models;
using StudioWebApp.ViewModels;
using StudioWebApp.ViewModels.TeamVM;

namespace StudioWebApp.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]
public class TeamController : Controller
{
    private readonly StudioDbContext _studioDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public TeamController(StudioDbContext studioDbContext,IWebHostEnvironment webHostEnvironment)
    {
        _studioDbContext = studioDbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task< IActionResult> Index(int page=1,int take=5)
    {
        List<Team> teams = await _studioDbContext.Teams.Skip((page-1)*take).Take(take).Include(t => t.Job).ToListAsync();
        int allTeamCount=_studioDbContext.Teams.Count();

        PaginationVM<Team> pagination = new PaginationVM<Team>()
        {
            Teams = teams,
            CurrentPage = page,
            PageCount = (int)Math.Ceiling((double)allTeamCount / take),
        };
        return View(pagination);
    }
    public async Task<IActionResult> Create()
    {
        CreateTeamVM createTeamVM = new CreateTeamVM()
        { 
         Jobs  =await _studioDbContext.Jobs.ToListAsync()
        };
        return View(createTeamVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTeamVM createTeam)
    {
        if (!ModelState.IsValid)
        {
            createTeam.Jobs = await _studioDbContext.Jobs.ToListAsync();
            return View(createTeam);
        }

        Team team=new Team()
        {
            Name = createTeam.Name,
            Surname = createTeam.Surname,
            JobId = createTeam.JobId
        };

        if(!createTeam.ProfileImage.ContentType.Contains("image/") && createTeam.ProfileImage.Length / 1024 > 2048)
        {
            ModelState.AddModelError("", "Incorrect image size or type!");
            createTeam.Jobs = await _studioDbContext.Jobs.ToListAsync();
            return View(createTeam);
        }
        string newFileName = Guid.NewGuid().ToString() + createTeam.ProfileImage.FileName;
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img",newFileName);
        using(FileStream fileStream =new FileStream(path, FileMode.CreateNew))
        {
            await createTeam.ProfileImage.CopyToAsync(fileStream);
        }
        team.ProfileImageName = newFileName;

        await _studioDbContext.Teams.AddAsync(team);
        await _studioDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        Team? team = await _studioDbContext.Teams.FindAsync(id);
        if(team == null) return NotFound();

        UpdateTeamVM updateTeamVM = new UpdateTeamVM()
        {
            Jobs = await _studioDbContext.Jobs.ToListAsync(),
            ProfileImageName = team.ProfileImageName,
            Name = team.Name,
            Surname = team.Surname,
            JobId = team.JobId
        };
        return View(updateTeamVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id , UpdateTeamVM updateTeamVM)
    {
        if (!ModelState.IsValid)
        {
            updateTeamVM.Jobs = await _studioDbContext.Jobs.ToListAsync();
            return View(updateTeamVM);
        }

        Team? team = await _studioDbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null) return NotFound();

        if(updateTeamVM.ProfileImage != null)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", team.ProfileImageName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await updateTeamVM.ProfileImage.CopyToAsync(fileStream);
            }
            updateTeamVM.ProfileImageName= team.ProfileImageName;
        }
        else updateTeamVM.ProfileImageName = team.ProfileImageName;

        team.Id= id;
        team.Surname = updateTeamVM.Surname;
        team.Name = updateTeamVM.Name;
        team.JobId = updateTeamVM.JobId;
        team.ProfileImageName = updateTeamVM.ProfileImageName;

        _studioDbContext.Teams.Update(team);
        await _studioDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        Team? team = await _studioDbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (team == null) return NotFound();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", team.ProfileImageName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
         _studioDbContext.Teams.Remove(team);
         await _studioDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Read(int id)
    {
        Team? team = _studioDbContext.Teams.Include(t=>t.Job).FirstOrDefault(t=>t.Id==id);
        if (team == null) return NotFound();

        return View(team);
    }
}
