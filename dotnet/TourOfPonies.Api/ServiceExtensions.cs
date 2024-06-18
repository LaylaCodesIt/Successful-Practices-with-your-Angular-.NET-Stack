using TourOfPonies.Api.Data;

namespace TourOfPonies.Api;

public static class ServiceExtensions
{
	public static IServiceCollection AddDependencies(this IServiceCollection services)
	{
		services.AddScoped<TableStorageContext>();
		services.AddScoped<TableStorageSeed>();
		services.AddScoped<PonyService>();
		return services;
	}
}
