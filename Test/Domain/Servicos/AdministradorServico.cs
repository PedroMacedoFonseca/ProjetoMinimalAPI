using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.DB;

namespace Teste.Domain.Entidades;

[TestClass]
public class AdministradorServicoTeste
{
    private DBContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
        return new DBContexto(configuration);
    }

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        //Arrange
        var adm = new Administrador();

        var context = CriarContextoDeTeste();
        context.Database.EnsureDeleted(); // Deleta o banco de teste, se existir
        context.Database.EnsureCreated(); // Cria o banco com base no seu modelo

        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        //Act
        administradorServico.Incluir(adm);

        //Assert
        Assert.AreEqual(1, administradorServico.Todos(1).Count());
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("teste", adm.Senha);
        Assert.AreEqual("Adm", adm.Perfil);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        //Arrange
        var adm = new Administrador();

        var context = CriarContextoDeTeste();
        context.Database.EnsureDeleted(); // Deleta o banco de teste, se existir
        context.Database.EnsureCreated(); // Cria o banco com base no seu modelo

        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        //Act
        administradorServico.Incluir(adm);
        context.SaveChanges();
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        //Assert
        Assert.IsNotNull(admDoBanco);
        Assert.AreEqual(adm.Id, admDoBanco.Id);
    }
}