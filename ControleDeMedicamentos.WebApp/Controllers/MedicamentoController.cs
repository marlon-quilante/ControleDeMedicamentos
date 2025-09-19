using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class MedicamentoController : Controller
    {
        private readonly RepositorioMedicamentoEmSql repositorioMedicamento;
        private readonly RepositorioFornecedorEmSql repositorioFornecedor;

        // Inversão de controle
        public MedicamentoController(RepositorioMedicamentoEmSql repositorioMedicamento, RepositorioFornecedorEmSql repositorioFornecedor)
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioFornecedor = repositorioFornecedor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Medicamento> medicamentos = repositorioMedicamento.ObterRegistros();
            VisualizarMedicamentosViewModel visualizarVM = new VisualizarMedicamentosViewModel(medicamentos);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();

            CadastrarMedicamentoViewModel cadastrarVM = new CadastrarMedicamentoViewModel(fornecedoresDisponiveis);

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
            {
                List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();
                cadastrarVM = new CadastrarMedicamentoViewModel(fornecedoresDisponiveis);

                return View(cadastrarVM);
            }

            Fornecedor fornecedorSelecionado = repositorioFornecedor.ObterRegistroPorID(cadastrarVM.FornecedorID);

            Medicamento novoMedicamento = new Medicamento(cadastrarVM.Nome, cadastrarVM.Descricao, fornecedorSelecionado);

            if (repositorioMedicamento.RegistroDuplicado(novoMedicamento))
            {
                List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();
                cadastrarVM = new CadastrarMedicamentoViewModel(fornecedoresDisponiveis);
                return View(cadastrarVM);
            }

            repositorioMedicamento.Cadastrar(novoMedicamento);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(id);

            List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();

            EditarMedicamentoViewModel editarVM = new EditarMedicamentoViewModel(medicamento.Id, medicamento.Nome, medicamento.Descricao, fornecedoresDisponiveis);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(EditarMedicamentoViewModel editarVM)
        {
            if (!ModelState.IsValid)
            {
                Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(editarVM.Id);
                List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();
                editarVM = new EditarMedicamentoViewModel(medicamento.Id, medicamento.Nome, medicamento.Descricao, fornecedoresDisponiveis);

                return View(editarVM);
            }

            Fornecedor fornecedorSelecionado = repositorioFornecedor.ObterRegistroPorID(editarVM.FornecedorID);

            Medicamento medicamentoAtualizado = new Medicamento(editarVM.Nome, editarVM.Descricao, fornecedorSelecionado);
            medicamentoAtualizado.Id = editarVM.Id;

            if (repositorioMedicamento.RegistroDuplicado(medicamentoAtualizado))
            {
                Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(editarVM.Id);
                List<Fornecedor> fornecedoresDisponiveis = repositorioFornecedor.ObterRegistros();
                editarVM = new EditarMedicamentoViewModel(medicamento.Id, medicamento.Nome, medicamento.Descricao, fornecedoresDisponiveis);

                return View(editarVM);
            }

            repositorioMedicamento.Editar(editarVM.Id, medicamentoAtualizado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(id);

            ExcluirMedicamentoViewModel excluirVM = new ExcluirMedicamentoViewModel(medicamento.Id, medicamento.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioMedicamento.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
