using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public HomeController(
        ApplicationDbContext dbContext,
        ILogger<HomeController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        List<TemperatureModel> temperature = await _dbContext.Temperatures.OrderByDescending(t => t.Id).Take(5).ToListAsync();
        HomePageViewModel model = new(temperature, FridgeStateModel.IsCompressorRunning);
        return View(model);
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
