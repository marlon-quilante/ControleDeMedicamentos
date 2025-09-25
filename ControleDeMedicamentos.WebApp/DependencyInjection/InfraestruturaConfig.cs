using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPrescricao;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleDeMedicamentos.WebApp.DependencyInjection
{
    public static class InfraestruturaConfig
    {
        public static void AddCamadaInfraestrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(_ => 
            {
                var connectionString = configuration["SQL_CONNECTION_STRING"];

                return new SqlConnection(connectionString); 
            });

            services.AddScoped<RepositorioFuncionarioEmSql>();
            services.AddScoped<RepositorioPacienteEmSql>();
            services.AddScoped<RepositorioFornecedorEmSql>();
            services.AddScoped<RepositorioMedicamentoEmSql>();
            services.AddScoped<RepositorioPrescricaoEmSql>();

            services.AddScoped<RepositorioFuncionarioEmArquivo>();    // Injeta um serviço por requisição HTTP
            services.AddScoped<RepositorioPacienteEmArquivo>();
            services.AddScoped<RepositorioFornecedorEmArquivo>();
            services.AddScoped<RepositorioMedicamentoEmArquivo>();
            services.AddScoped<RepositorioPrescricaoEmArquivo>();
            services.AddScoped<RepositorioEntradaMedicamentoEmArquivo>();
        }
    }
}
