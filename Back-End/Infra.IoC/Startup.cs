using Crosscutting.Interfaces.Log;
using Domain._Base.Interfaces;
using Domain._Base.Servicos;
using Domain.Comida.Interfaces;
using Domain.Comida.Services;
using Domain.Comida.Validators;
using Domain.Comidas.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.Log._Base;
using Infra.Log.Interfaces;
using Infra.Log.Loggers;
using Infra.Log.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDeliveryDbContext(configuration);

        // Services
        services
            .AddScoped<IListagemProdutosService, ListagemProdutosService>()
            .AddScoped<IListagemIngredientesService, ListagemIngredientesService>()
            .AddScoped<IListagemItensExtrasService, ListagemItensExtrasService>();
        
        // Repositories
            services
            .AddScoped<IProdutoRepository, ProdutoRepository>()
            .AddScoped<IIngredienteRepository, IngredienteRepository>()
            .AddScoped<IItemExtraRepository, ItemExtraRepository>();
        
        // Validators
            services
            .AddScoped<ICadastrarIngredienteCommandValidator, CadastrarIngredienteCommandValidator>()
            .AddScoped<ICadastrarItemExtraCommandValidator, CadastrarItemExtraCommandValidator>()
            .AddScoped<ICadastrarProdutoCommandValidator, CadastrarProdutoCommandValidator>();
        
        // Logging
        services
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<ISingletonLoggerWrapper, SingletonLoggerWrapper>()
            .AddScoped<ILoggerPadrao, LoggerPadrao>()
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<ILogWriter, LogWriter>();
        
        // Configs
        services
            .AddScoped<IErroDeDominioHandler, ErroDeDominioHandler>();


    }

    private static void AddDeliveryDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<DeliveryDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DeliveryDatabase"),
                new MySqlServerVersion(new Version(8, 0, 23)),
                providerOptions => providerOptions
                    .MigrationsHistoryTable("_deliverymigrations")));
}