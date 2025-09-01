using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class PrescricaoController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly RepositorioPrescricaoEmArquivo repositorioPrescricao;
        private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
        private readonly RepositorioPacienteEmArquivo repositorioPaciente;

        public PrescricaoController(ContextoDados contextoDados, RepositorioPrescricaoEmArquivo repositorioPrescricao, RepositorioMedicamentoEmArquivo repositorioMedicamento, RepositorioPacienteEmArquivo repositorioPaciente)
        {
            this.contextoDados = contextoDados;
            this.repositorioPrescricao = repositorioPrescricao;
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioPaciente = repositorioPaciente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var prescricoes = repositorioPrescricao.ObterRegistros();

            var visualizarVM = new VisualizarPrescricoesViewModel(prescricoes);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var pacientes = repositorioPaciente.ObterRegistros();

            var cadastrarVM = new CadastrarPrescricaoViewModel(pacientes);

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarPrescricaoViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
            {
                var pacientes = repositorioPaciente.ObterRegistros();
                cadastrarVM = new CadastrarPrescricaoViewModel(pacientes);
                return View(cadastrarVM);
            }

            var pacienteSelecionado = repositorioPaciente.ObterRegistroPorID(cadastrarVM.PacienteID);

            var novaPrescricao = new Prescricao(cadastrarVM.Descricao, pacienteSelecionado, cadastrarVM.DataValidade, cadastrarVM.CrmMedico);

            repositorioPrescricao.Cadastrar(novaPrescricao);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GerenciarMedicamentosPrescritos(Guid id)
        {
            var prescricaoSelecionada = repositorioPrescricao.ObterRegistroPorID(id);

            var medicamentos = repositorioMedicamento.ObterRegistros();

            var gerenciarMedicamentosVM = new GerenciarMedicamentosPrescritosViewModel(prescricaoSelecionada.Id, prescricaoSelecionada.Descricao, prescricaoSelecionada.CrmMedico, prescricaoSelecionada.Paciente.Id, prescricaoSelecionada.Paciente, medicamentos, prescricaoSelecionada.MedicamentosPrescritos);

            return View(gerenciarMedicamentosVM);
        }

        [HttpPost]
        public IActionResult AdicionarMedicamentoPrescrito(Guid idPrescricao, AdicionarMedicamentoPrescritoViewModel adicionarMedicamentoVM)
        {
            var prescricaoSelecionada = repositorioPrescricao.ObterRegistroPorID(idPrescricao);

            var medicamentoSelecionado = repositorioMedicamento.ObterRegistroPorID(adicionarMedicamentoVM.MedicamentoID);

            prescricaoSelecionada.AdicionarMedicamentoPrescrito(new MedicamentoPrescrito(medicamentoSelecionado, adicionarMedicamentoVM.DosagemMedicamento, adicionarMedicamentoVM.PeriodoMedicamento, adicionarMedicamentoVM.QuantidadeMedicamento));

            contextoDados.Salvar();

            return RedirectToAction(nameof(GerenciarMedicamentosPrescritos), new { id = idPrescricao });
        }

        [HttpPost]
        public IActionResult RemoverMedicamentoPrescrito(Guid idPrescricao, Guid idMedicamentoPrescrito)
        {
            Prescricao prescricaoSelecionada = repositorioPrescricao.ObterRegistroPorID(idPrescricao);

            prescricaoSelecionada.RemoverMedicamentoPrescrito(idMedicamentoPrescrito);

            contextoDados.Salvar();

            return RedirectToAction(nameof(GerenciarMedicamentosPrescritos), new { id = idPrescricao });
        }
    }
}
