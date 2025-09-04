using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class EntradaSaidaMedicamentoController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
        private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;
        private readonly RepositorioEntradaMedicamentoEmArquivo repositorioEntradaMedicamento;

        public EntradaSaidaMedicamentoController()
        {
            contextoDados = new ContextoDados(true);
            repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
            repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);
            repositorioEntradaMedicamento = new RepositorioEntradaMedicamentoEmArquivo(contextoDados);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EntradaMedicamento> entradasMedicamento = repositorioEntradaMedicamento.ObterRegistros();

            VisualizarEntradasMedicamentoViewModel visualizarVM = new VisualizarEntradasMedicamentoViewModel(entradasMedicamento);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult RegistrarEntrada()
        {
            List<Medicamento> medicamentosDisponiveis = repositorioMedicamento.ObterRegistros();
            List<Funcionario> funcionariosDisponiveis = repositorioFuncionario.ObterRegistros(); 

            CadastrarEntradaMedicamentoViewModel cadastrarVM = new CadastrarEntradaMedicamentoViewModel(medicamentosDisponiveis, funcionariosDisponiveis);

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult RegistrarEntrada(CadastrarEntradaMedicamentoViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
            {
                List<Medicamento> medicamentosDisponiveis = repositorioMedicamento.ObterRegistros();
                List<Funcionario> funcionariosDisponiveis = repositorioFuncionario.ObterRegistros();

                cadastrarVM = new CadastrarEntradaMedicamentoViewModel(medicamentosDisponiveis, funcionariosDisponiveis);

                return View(cadastrarVM);
            }

            Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(cadastrarVM.MedicamentoID);
            Funcionario funcionario = repositorioFuncionario.ObterRegistroPorID(cadastrarVM.FuncionarioID);

            EntradaMedicamento entradaMedicamento = new EntradaMedicamento(medicamento, funcionario, cadastrarVM.QtdEntrada);

            repositorioEntradaMedicamento.Cadastrar(entradaMedicamento);

            return RedirectToAction(nameof(Index));
        }
    }
}
