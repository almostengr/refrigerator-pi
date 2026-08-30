using Almostengr.RefrigeratorPi.Features.Compressors.Domain;
using Almostengr.RefrigeratorPi.Features.SystemSettings.Domain;
using Almostengr.RefrigeratorPi.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.RefrigeratorPi.Features.Common.Shared;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CompressorHistory> CompressorHistories { get; set; }
    public DbSet<SystemSettingEntity> SystemSettings { get; set; }
    public DbSet<TemperatureModel> Temperatures { get; set; }
}
