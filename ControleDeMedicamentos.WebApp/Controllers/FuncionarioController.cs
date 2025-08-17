using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;

        // Inversão de controle
        public FuncionarioController(RepositorioFuncionarioEmArquivo repositorioFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
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
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Funcionario novoFuncionario = new Funcionario(cadastrarVM.Nome, cadastrarVM.Telefone, cadastrarVM.CPF);

            if (repositorioFuncionario.RegistroDuplicado(novoFuncionario))
                return View(cadastrarVM);

            repositorioFuncionario.Cadastrar(novoFuncionario);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            Funcionario funcionario = repositorioFuncionario.ObterRegistroPorID(id);

            EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(funcionario.Id, funcionario.Nome, funcionario.Telefone, funcionario.CPF);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(EditarFuncionarioViewModel editarVM)
        {
            if (!ModelState.IsValid)
                return View(editarVM);

            Funcionario funcionarioAtualizado = new Funcionario(editarVM.Nome, editarVM.Telefone, editarVM.CPF);
            funcionarioAtualizado.Id = editarVM.Id;

            if (repositorioFuncionario.RegistroDuplicado(funcionarioAtualizado))
                return View(editarVM);

            repositorioFuncionario.Editar(editarVM.Id, funcionarioAtualizado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            Funcionario funcionario = repositorioFuncionario.ObterRegistroPorID(id);

            ExcluirFuncionarioViewModel excluirVM = new ExcluirFuncionarioViewModel(funcionario.Id, funcionario.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioFuncionario.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
