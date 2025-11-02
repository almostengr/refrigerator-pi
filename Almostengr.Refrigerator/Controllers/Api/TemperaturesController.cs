using Almostengr.Refrigerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Controllers.Api;

public class TemperaturesController : BaseAppApiController
{
    private readonly ApplicationDbContext _dbContext;

    public TemperaturesController(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<TemperatureModel> model = await _dbContext.Temperatures.OrderByDescending(t => t.Id).Take(50).ToListAsync();
        return Ok(model);
    }
}
