using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.Models;
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

        [HttpGet]
        public IActionResult Index()
        {
            List<Funcionario> funcionarios = repositorioFuncionario.ObterRegistros();
            VisualizarFuncionariosViewModel visualizarVM = new VisualizarFuncionariosViewModel(funcionarios);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CadastrarFuncionarioViewModel cadastrarVM = new CadastrarFuncionarioViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVM)
        {
            Funcionario novoFuncionario = new Funcionario(cadastrarVM.Nome, cadastrarVM.Telefone, cadastrarVM.CPF);

            repositorioFuncionario.Cadastrar(novoFuncionario);

            return RedirectToAction(nameof(Index));
        }
    }
}
