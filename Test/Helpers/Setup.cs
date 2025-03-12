using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.Servicos;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient cliente = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<Startup>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
            });
        });
    }
    public static void ClassCleanUp()
    {
        if (http != null)
        {
            http.Dispose(); 
            cliente.Dispose();
        }
    }
}