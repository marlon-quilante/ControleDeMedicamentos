using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;

namespace ControleDeMedicamentos.WebApp.DependencyInjection
{
    public static class InfraestruturaConfig
    {
        public static void AddCamadaInfraestrutura(this IServiceCollection services)
        {
            services.AddScoped<RepositorioFuncionarioEmArquivo>();    // Injeta um serviço por requisição HTTP
            services.AddScoped<RepositorioPacienteEmArquivo>();
            services.AddScoped<RepositorioFornecedorEmArquivo>();
            services.AddScoped<RepositorioMedicamentoEmArquivo>();
            services.AddScoped<RepositorioPrescricaoEmArquivo>();
            services.AddScoped<RepositorioEntradaMedicamentoEmArquivo>();
        }
    }
}
