using Microsoft.AspNetCore.Mvc;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Controllers;

public class TemperatureController : BaseController{
    private readonly ApplicationDbContext _dbContext;

    public TemperatureController(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<TemperatureModel> model = await _dbContext.Temperatures.OrderByDescending(t => t.Id).ToListAsync();
        return View(model);
    }
}
