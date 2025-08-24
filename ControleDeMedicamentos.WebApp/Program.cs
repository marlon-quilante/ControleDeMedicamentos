using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Injeção de dependências
            builder.Services.AddScoped((_) => new ContextoDados(true));       //Expressão Lambda

            builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();    // Injeta um serviço por requisição HTTP
            builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
            builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
            builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
            // builder.Services.AddSingleton();                               // Instancia uma vez o serviço e injeta em todas as requisições
            // builder.Services.AddTransient();                               // Instancia o serviço toda vez que for chamado em uma requisição

            var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.log");

            //Variáveis de ambiente
            var licenseKey = builder.Configuration["NEWRELIC_LICENSE_KEY"];

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
                .WriteTo.NewRelicLogs(
                    endpointUrl: "https://log-api.newrelic.com/log/v1",
                    applicationName: "controle-de-medicamentos",
                    licenseKey: licenseKey
                )
                .CreateLogger();

            builder.Logging.ClearProviders();

            builder.Services.AddSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
