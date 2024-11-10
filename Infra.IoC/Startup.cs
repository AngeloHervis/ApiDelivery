using Crosscutting.Interfaces;
using Crosscutting.Interfaces.Log;
using Crosscutting.UsuarioLogado;
using Domain._Base.Interfaces;
using Domain.Comida.Interfaces;
using Domain.Comida.Validators;
using Infra.Data;
using Infra.Data.Repository;
using Infra.Log._Base;
using Infra.Log.Interfaces;
using Infra.Log.Loggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDeliveryDbContext(configuration);
        services.AddEventStoreDbContext(configuration);

        services
            // .AddScoped<IEventStore, EventStore>()
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<IUsuarioLogadoService, UsuarioLogadoService>()
            .AddScoped<ICadastrarProdutoCommandValidator, CadastrarProdutoCommandValidator>()
            .AddScoped<IProdutoRepository, ProdutoRepository>();
        
        //Logging
        services
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<ILoggerPadrao, LoggerPadrao>()
            .AddScoped<ILogWriter, LogWriter>();
    }

    private static void AddDeliveryDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<DeliveryDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DeliveryDatabase"),
                new MySqlServerVersion(new Version(8, 0, 23)),
                providerOptions => providerOptions
                    .MigrationsHistoryTable("_deliverymigrations")));

    private static void AddEventStoreDbContext(this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<EventStoreContext>(options =>
            options.UseMySql(
                configuration["ConnectionStrings:DeliveryDatabase"],
                new MySqlServerVersion(new Version(8, 0, 23)),
                providerOptions => providerOptions
                    .MigrationsHistoryTable("_eventstoremigrations")));
}