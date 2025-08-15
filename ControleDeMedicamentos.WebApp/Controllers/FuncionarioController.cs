using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly RepositorioFuncionario repositorioFuncionario;
        private ContextoDados contextoDados;

        public FuncionarioController()
        {
            contextoDados = new ContextoDados(carregarDados: true);
            repositorioFuncionario = new RepositorioFuncionario(contextoDados);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
