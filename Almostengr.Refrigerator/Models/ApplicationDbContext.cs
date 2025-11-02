using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SystemSettingModel> SystemSettings { get; set; }
    public DbSet<TemperatureModel> Temperatures { get; set; }
}