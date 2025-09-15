using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFornecedor;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly RepositorioFornecedorEmSql repositorioFornecedor;

        // Inversão de controle
        public FornecedorController(RepositorioFornecedorEmSql repositorioFornecedor)
        {
            this.repositorioFornecedor = repositorioFornecedor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Fornecedor> fornecedores = repositorioFornecedor.ObterRegistros();
            VisualizarFornecedoresViewModel visualizarVM = new VisualizarFornecedoresViewModel(fornecedores);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CadastrarFornecedorViewModel cadastrarVM = new CadastrarFornecedorViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Fornecedor novoFornecedor = new Fornecedor(cadastrarVM.Nome, cadastrarVM.Telefone, cadastrarVM.CNPJ);

            if (repositorioFornecedor.RegistroDuplicado(novoFornecedor))
                return View(cadastrarVM);

            repositorioFornecedor.Cadastrar(novoFornecedor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            Fornecedor fornecedor = repositorioFornecedor.ObterRegistroPorID(id);

            EditarFornecedorViewModel editarVM = new EditarFornecedorViewModel(fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.CNPJ);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(EditarFornecedorViewModel editarVM)
        {
            if (!ModelState.IsValid)
                return View(editarVM);

            Fornecedor fornecedorAtualizado = new Fornecedor(editarVM.Nome, editarVM.Telefone, editarVM.CNPJ);
            fornecedorAtualizado.Id = editarVM.Id;

            if (repositorioFornecedor.RegistroDuplicado(fornecedorAtualizado))
                return View(editarVM);

            repositorioFornecedor.Editar(editarVM.Id, fornecedorAtualizado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            Fornecedor fornecedor = repositorioFornecedor.ObterRegistroPorID(id);

            ExcluirFornecedorViewModel excluirVM = new ExcluirFornecedorViewModel(fornecedor.Id, fornecedor.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioFornecedor.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
