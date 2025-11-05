using Microsoft.AspNetCore.Mvc;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Services.Interfaces;

namespace Almostengr.Refrigerator.Controllers;

public class SystemSettingController : BaseController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SystemSettingController> _logger;
    private readonly ISystemSettingService _systemSettingService;

    public SystemSettingController(
        ApplicationDbContext dbContext,
        ILogger<SystemSettingController> logger,
        ISystemSettingService systemSettingservice
        )
    {
        _dbContext = dbContext;
        _logger = logger;
        _systemSettingService = systemSettingservice;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<SystemSettingModel> model = await _dbContext.SystemSettings.OrderBy(s => s.Id).ToListAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        SystemSettingModel model = await _systemSettingService.GetEntityByIdAsync(id);
        if (model == null)
        {
            return NotFoundParitalView();
        }

        return PartialView("_Edit", model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SystemSettingModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                SystemSettingModel entity = await _systemSettingService.GetEntityByIdAsync(model.Id);
                if (entity == null)
                {
                    return NotFoundParitalView();
                }

                Result<SystemSettingModel> result = model.AssignToEntity(entity, SYSTEM_USER);
                if (result.Succeeded)
                {
                    _dbContext.SystemSettings.Update(result.Value);
                    await _dbContext.SaveChangesAsync();
                    return NoContent();
                }
                AddErrorsToModelState(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return PartialView("_Edit", model);
    }
}
