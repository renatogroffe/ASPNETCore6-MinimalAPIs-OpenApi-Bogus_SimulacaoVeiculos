using Bogus;
using Bogus.Extensions.Brazil;
using APIVeiculos.Models;

namespace APIVeiculos.Data;

public class VeiculosRepository
{
    private readonly List<Veiculo> _veiculos;

    public VeiculosRepository(
        ILogger<VeiculosRepository> logger,
        IConfiguration configuration)
    {
        var numeroVeiculos = Convert.ToInt32(configuration["NumeroVeiculos"]);
        logger.LogInformation($"Gerando {numeroVeiculos} registro(s) de veículo(s)...");
        _veiculos = new Faker<Veiculo>("pt_BR").StrictMode(true)
            .RuleFor(p => p.Modelo, f => f.Vehicle.Model())
            .RuleFor(p => p.Proprietario, f => f.Name.FullName())
            .RuleFor(p => p.CPF, f => f.Person.Cpf(includeFormatSymbols: true))
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Proprietario).ToLower())
            .Generate(numeroVeiculos);
    }

    public List<Veiculo> GetAll() => _veiculos;
}