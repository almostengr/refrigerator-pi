using Almostengr.Refrigerator.Features.Compressors.Domain;
using Almostengr.Refrigerator.Features.SystemSettings.Domain;
using Almostengr.Refrigerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.Common.Shared;

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
