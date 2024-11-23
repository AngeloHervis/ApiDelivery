using Crosscutting.Interfaces;
using Crosscutting.Interfaces.Log;
using Crosscutting.UsuarioLogado;
using Domain._Base.Interfaces;
using Domain._Base.Servicos;
using Domain.Autenticacao.Interfaces;
using Domain.Autenticacao.Services;
using Domain.Comida.Commands;
using Domain.Comida.Interfaces;
using Domain.Comida.Services;
using Domain.Comida.Validators;
using Domain.Usuarios.Interfaces;
using Domain.Usuarios.Services;
using Infra.Data;
using Infra.Data.EventSourcing;
using Infra.Data.Repositories;
using Infra.Data.Repository;
using Infra.Data.Repository._Base;
using Infra.Data.Repository.EventSourcing;
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
        services.AddEventStoreDbContext(configuration);

        // Services
        services
            .AddScoped<IUsuarioLogadoService, UsuarioLogadoService>()
            .AddScoped<ILoginUsuarioService, LoginUsuarioService>()
            .AddScoped<IRedefinirSenhaUsuarioService, RedefinirSenhaUsuarioService>()
            .AddScoped<IListagemProdutosService, ListagemProdutosService>()
            .AddScoped<IListagemIngredientesService, ListagemIngredientesService>()
            .AddScoped<IListagemItensExtrasService, ListagemItensExtrasService>();
        
        // Repositories
            services
            .AddScoped<IUsuarioRepository, UsuarioRepository>()
            .AddScoped<IProdutoRepository, ProdutoRepository>()
            .AddScoped<IIngredienteRepository, IngredienteRepository>()
            .AddScoped<IItemExtraRepository, ItemExtraRepository>();
        
        // Validators
            services
            .AddScoped<ICadastrarIngredienteCommandValidator, CadastrarIngredienteCommandValidator>()
            .AddScoped<ICadastrarItemExtraCommandValidator, CadastrarItemExtraCommandValidator>()
            .AddScoped<ICadastrarProdutoCommandValidator, CadastrarProdutoCommandValidator>();
        
        // EventSourcing
        services
            .AddScoped<IStoredEventRepository, StoredEventRepository>()
            .AddScoped<IEventStore, EventStore>();
        
        // Logging
        services
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<ISingletonLoggerWrapper, SingletonLoggerWrapper>()
            .AddScoped<ILoggerPadrao, LoggerPadrao>()
            .AddScoped<ILoggerServicosDeDominio, LoggerServicosDeDominio>()
            .AddScoped<ILogWriter, LogWriter>();
        
        // Configs
        services
            .AddScoped<IErroDeDominioHandler, ErroDeDominioHandler>()
            .AddScoped<ITokenService, TokenService>();


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