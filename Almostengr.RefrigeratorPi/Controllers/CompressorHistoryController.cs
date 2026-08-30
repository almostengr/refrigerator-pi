using Microsoft.AspNetCore.Mvc;
using Almostengr.Common.Controllers;
using Almostengr.RefrigeratorPi.Features.Compressors.DomainServices.Interfaces;

namespace Almostengr.RefrigeratorPi.Controllers;

public class CompressorHistoryController : BaseUiController
{
    private readonly IQueryCompressorHistoryService _queryCompressorHistoryService;

    public CompressorHistoryController(
        IQueryCompressorHistoryService queryCompressorHistoryService
    )
    {
        _queryCompressorHistoryService = queryCompressorHistoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var resource = await _queryCompressorHistoryService.GetListAsync();
        return View(resource);
    }
}