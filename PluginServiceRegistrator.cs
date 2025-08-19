using Microsoft.Extensions.DependencyInjection;
using Jellyzam.Services;

namespace Jellyzam;

/// <summary>
/// Server entry point for the Jellyzam plugin.
/// </summary>
public class PluginServiceRegistrator
{
    /// <summary>
    /// Registers services for the plugin.
    /// </summary>
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<ShazamService>();
        serviceCollection.AddScoped<MusicIdentificationService>();
        serviceCollection.AddScoped<FileOrganizationService>();
    }
}
