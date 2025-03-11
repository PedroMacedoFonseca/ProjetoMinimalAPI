using MinimalAPI.Dominio.Entidades;

namespace Teste.Domain.Entidades;

[TestClass]
public sealed class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var veiculo = new Veiculo();

        //Act
        veiculo.Id = 1;
        veiculo.Nome = "Eclipse";
        veiculo.Marca = "Mistubishi";
        veiculo.Ano = 2009;

        //Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Eclipse", veiculo.Nome);
        Assert.AreEqual("Mistubishi", veiculo.Marca);
        Assert.AreEqual(2009, veiculo.Ano);
    }
}