using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly RepositorioPacienteEmSql repositorioPaciente;

        // Inversão de controle
        public PacienteController(RepositorioPacienteEmSql repositorioPaciente)
        {
            this.repositorioPaciente = repositorioPaciente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Paciente> pacientes = repositorioPaciente.ObterRegistros();
            VisualizarPacientesViewModel visualizarVM = new VisualizarPacientesViewModel(pacientes);

            return View(visualizarVM);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CadastrarPacienteViewModel cadastrarVM = new CadastrarPacienteViewModel();

            return View(cadastrarVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVM)
        {
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            Paciente novoPaciente = new Paciente(cadastrarVM.Nome, cadastrarVM.Telefone, cadastrarVM.NumCartaoSUS, cadastrarVM.CPF);

            if (repositorioPaciente.RegistroDuplicado(novoPaciente))
                return View(cadastrarVM);

            repositorioPaciente.Cadastrar(novoPaciente);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            Paciente paciente = repositorioPaciente.ObterRegistroPorID(id);

            EditarPacienteViewModel editarVM = new EditarPacienteViewModel(paciente.Id, paciente.Nome, paciente.Telefone, paciente.NumCartaoSUS, paciente.CPF);

            return View(editarVM);
        }

        [HttpPost]
        public IActionResult Editar(EditarPacienteViewModel editarVM)
        {
            if (!ModelState.IsValid)
                return View(editarVM);

            Paciente pacienteAtualizado = new Paciente(editarVM.Nome, editarVM.Telefone, editarVM.NumCartaoSUS, editarVM.CPF);
            pacienteAtualizado.Id = editarVM.Id;

            if (repositorioPaciente.RegistroDuplicado(pacienteAtualizado))
                return View(editarVM);

            repositorioPaciente.Editar(editarVM.Id, pacienteAtualizado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            Paciente paciente = repositorioPaciente.ObterRegistroPorID(id);

            ExcluirPacienteViewModel excluirVM = new ExcluirPacienteViewModel(paciente.Id, paciente.Nome);

            return View(excluirVM);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioPaciente.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
