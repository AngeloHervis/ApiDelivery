using System.Reflection;
using System.Text.Json.Serialization;
using ApiDelivery.Middleware;
using Infra.IoC;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

const string origemPermitida = "_origemPermitida";
builder.Services.AddCors(options =>
{
    options.AddPolicy(origemPermitida,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddLogging(x => { x.AddConsole(); });

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Delivery",
        Version = "v1"
    });

    c.ExampleFilters();

    c.CustomSchemaIds(x => x.FullName);

    const string xmlFile = "Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

// Swagger examples
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());


// HttpClient
builder.Services.AddHttpClient();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

// Add Application Insights telemetry
builder.Services.AddApplicationInsightsTelemetry();

// Add Services
Startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

await Infra.Data.Startup.RunMigration(app);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery API"); });    
}

app.UseHttpsRedirection();

app.UseCors(origemPermitida);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

await app.RunAsync();