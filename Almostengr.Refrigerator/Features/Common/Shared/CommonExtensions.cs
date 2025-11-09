using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Refrigerator.Features.Common.Shared;


public static class CommonExtensions
{
    public static string SYSTEM_USER = "System";
    public static string LOG_MESSAGE = "{message}";

    public static string ToDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
        {
            return enumValue.ToString();
        }
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute?.Description ?? enumValue.ToString();
    }

    public static string ToOnOff(this bool value)
    {
        return value ? "On" : "Off";
    }

    public static void AddCommonExtensions(this IServiceCollection services, IConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString, sqlite => sqlite.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }
}