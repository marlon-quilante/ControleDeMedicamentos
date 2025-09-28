using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPrescricao;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class EntradaSaidaMedicamentoController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly RepositorioMedicamentoEmSql repositorioMedicamento;
        private readonly RepositorioFuncionarioEmSql repositorioFuncionario;
        private readonly RepositorioEntradaMedicamentoEmSql repositorioEntradaMedicamento;
        private readonly RepositorioSaidaMedicamentoEmSql repositorioSaidaMedicamento;
        private readonly RepositorioPacienteEmSql repositorioPaciente;
        private readonly RepositorioPrescricaoEmSql repositorioPrescricao;

        public EntradaSaidaMedicamentoController(
            RepositorioEntradaMedicamentoEmSql repositorioEntradaMedicamento, 
            RepositorioMedicamentoEmSql repositorioMedicamento, 
            RepositorioFuncionarioEmSql repositorioFuncionario,
            RepositorioSaidaMedicamentoEmSql repositorioSaidaMedicamento,
            RepositorioPacienteEmSql repositorioPaciente,
            RepositorioPrescricaoEmSql repositorioPrescricao)
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioEntradaMedicamento = repositorioEntradaMedicamento;
            this.repositorioSaidaMedicamento = repositorioSaidaMedicamento;
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioPrescricao = repositorioPrescricao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EntradaMedicamento> entradasMedicamento = repositorioEntradaMedicamento.ObterRegistros();
            List<SaidaMedicamento> saidasMedicamento = repositorioSaidaMedicamento.ObterRegistros();

            VisualizarEntradasSaidasMedicamentoViewModel visualizarVM = new VisualizarEntradasSaidasMedicamentoViewModel(entradasMedicamento, saidasMedicamento);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult RegistrarEntrada()
        {
            List<Medicamento> medicamentosDisponiveis = repositorioMedicamento.ObterRegistros();
            List<Funcionario> funcionariosDisponiveis = repositorioFuncionario.ObterRegistros(); 

            RegistrarEntradaMedicamentoViewModel cadastrarVM = new RegistrarEntradaMedicamentoViewModel(medicamentosDisponiveis, funcionariosDisponiveis);

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult RegistrarEntrada(RegistrarEntradaMedicamentoViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
            {
                List<Medicamento> medicamentosDisponiveis = repositorioMedicamento.ObterRegistros();
                List<Funcionario> funcionariosDisponiveis = repositorioFuncionario.ObterRegistros();

                cadastrarVM = new RegistrarEntradaMedicamentoViewModel(medicamentosDisponiveis, funcionariosDisponiveis);

                return View(cadastrarVM);
            }

            Medicamento medicamento = repositorioMedicamento.ObterRegistroPorID(cadastrarVM.MedicamentoID);
            Funcionario funcionario = repositorioFuncionario.ObterRegistroPorID(cadastrarVM.FuncionarioID);

            EntradaMedicamento entradaMedicamento = new EntradaMedicamento(medicamento, funcionario, cadastrarVM.QtdEntrada);

            repositorioEntradaMedicamento.Cadastrar(entradaMedicamento);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RegistrarSaida()
        {
            List<Funcionario> funcionarios = repositorioFuncionario.ObterRegistros();

            DadosIniciaisSaidaMedicamentoViewModel dadosIniciaisSaidaVM = new DadosIniciaisSaidaMedicamentoViewModel(funcionarios);

            return View(dadosIniciaisSaidaVM);
        }

        [HttpPost]
        public IActionResult RegistrarSaida(DadosIniciaisSaidaMedicamentoViewModel dadosIniciaisSaidaVM)
        {
            if (!ModelState.IsValid)
            {
                List<Funcionario> funcionarios = repositorioFuncionario.ObterRegistros();
                dadosIniciaisSaidaVM = new DadosIniciaisSaidaMedicamentoViewModel(funcionarios);

                return View(dadosIniciaisSaidaVM);
            }

            Funcionario funcionarioSelecionado = repositorioFuncionario.ObterRegistroPorID(dadosIniciaisSaidaVM.FuncionarioID);
            Paciente pacienteSelecionado = repositorioPaciente.ObterRegistroPorCPF(dadosIniciaisSaidaVM.CPFPaciente);
            
            List<Prescricao> prescricoesPaciente = repositorioPrescricao.ObterPrescricoesPorPaciente(pacienteSelecionado);

            PrescricoesSaidaMedicamentoViewModel prescricaoSaidaVM = new PrescricoesSaidaMedicamentoViewModel
                (funcionarioSelecionado.Nome, 
                funcionarioSelecionado.Id, 
                pacienteSelecionado.Nome, 
                pacienteSelecionado.Id,
                pacienteSelecionado.Telefone,
                prescricoesPaciente);

            return View(nameof(RegistrarSaidaPrescricoes), prescricaoSaidaVM);
        }

        [HttpPost]
        public IActionResult RegistrarSaidaPrescricoes(Guid idPrescricao, Guid idFuncionario)
        {
            Prescricao prescricaoSelecionada = repositorioPrescricao.ObterRegistroPorID(idPrescricao);
            Funcionario funcionarioSelecionado = repositorioFuncionario.ObterRegistroPorID(idFuncionario);

            RegistrarSaidaMedicamentoViewModel registrarSaidaVM = new RegistrarSaidaMedicamentoViewModel
                (funcionarioSelecionado.Nome,
                funcionarioSelecionado.Id,
                prescricaoSelecionada.Paciente.Nome,
                prescricaoSelecionada.Paciente.Id,
                prescricaoSelecionada.Descricao,
                prescricaoSelecionada.Id,
                prescricaoSelecionada.MedicamentosPrescritos);

            return View(nameof(RegistrarSaidaConfirmado), registrarSaidaVM);
        }

        [HttpPost]
        public IActionResult RegistrarSaidaConfirmado(Guid idPrescricao, Guid idFuncionario)
        {
            Prescricao prescricaoSelecionada = repositorioPrescricao.ObterRegistroPorID(idPrescricao);
            Funcionario funcionarioSelecionado = repositorioFuncionario.ObterRegistroPorID(idFuncionario);

            SaidaMedicamento saidaMedicamento = new SaidaMedicamento(prescricaoSelecionada, funcionarioSelecionado);

            repositorioSaidaMedicamento.Cadastrar(saidaMedicamento);

            return RedirectToAction(nameof(Index));
        }
    }
}
