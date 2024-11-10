using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data;

public static class Startup
{
    public static async Task RunMigration(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<DeliveryDbContext>();
        var eventContext = serviceScope.ServiceProvider.GetService<EventStoreContext>();
        await context!.Database.MigrateAsync();
        await eventContext!.Database.MigrateAsync();
    }
}