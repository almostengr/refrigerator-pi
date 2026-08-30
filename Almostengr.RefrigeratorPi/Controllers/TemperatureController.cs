using Microsoft.AspNetCore.Mvc;
using Almostengr.RefrigeratorPi.Models;
using Almostengr.RefrigeratorPi.Features.Temperatures.Services.interfaces;

namespace Almostengr.RefrigeratorPi.Controllers;

public class TemperatureController : BaseController
{
    private readonly IQueryTemperatureService _queryTemperatureService;

    public TemperatureController(
        IQueryTemperatureService queryTemperatureService)
    {
        _queryTemperatureService = queryTemperatureService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IList<TemperatureModel> model = await _queryTemperatureService.GetListAsync();
        return View(model);
    }
}
