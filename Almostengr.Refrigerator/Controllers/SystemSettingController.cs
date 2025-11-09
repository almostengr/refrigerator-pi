using Microsoft.AspNetCore.Mvc;
using Almostengr.Refrigerator.Models;
using Almostengr.Common.DomainServices.Results;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Services.Interfaces;
using Almostengr.Refrigerator.Features.SystemSettings.DomainServices;

namespace Almostengr.Refrigerator.Controllers;

public class SystemSettingController : BaseController
{
    private readonly IQuerySystemSettingService _queryService;
    private readonly IUpdateSystemSettingService _updateService;

    public SystemSettingController(
        IQuerySystemSettingService queryService,
        IUpdateSystemSettingService updateService
        )
    {
        _queryService = queryService;
        _updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IList<SystemSettingEntity> model = await _queryService.GetListAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        SystemSettingEntity model = await _queryService.GetEntityByIdAsync(id);
        if (model == null)
        {
            return NotFoundParitalView();
        }

        return PartialView("_Edit", model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SystemSettingViewModel model)
    {
        if (ModelState.IsValid)
        {
            SystemSettingEntity entity = await _queryService.GetEntityByIdAsync(model.Id);
            if (entity == null)
            {
                return NotFoundParitalView();
            }

            Result<SystemSettingEntity> result = await _updateService.ExecuteAsync(entity);
            if (result.Succeeded)
            {
                return NoContent();
            }
            AddErrorsToModelState(result.Errors);
        }

        return PartialView("_Edit", model);
    }
}
