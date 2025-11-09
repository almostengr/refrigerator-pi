using Microsoft.AspNetCore.Mvc;
using Almostengr.Common.Controllers;
using Almostengr.Refrigerator.Features.Compressors.DomainServices.Interfaces;

namespace Almostengr.Refrigerator.Controllers;

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