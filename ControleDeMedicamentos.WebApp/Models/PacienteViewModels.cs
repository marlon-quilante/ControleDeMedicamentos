using ControleDeMedicamentos.Dominio.ModuloPaciente;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarPacientesViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string NumCartaoSUS { get; set; }
        public string CPF { get; set; }
        public List<DetalhesPacienteViewModel> Pacientes { get; set; }

        public VisualizarPacientesViewModel(List<Paciente> pacientes)
        {
            Pacientes = new List<DetalhesPacienteViewModel>();

            foreach (var p in pacientes)
            {
                DetalhesPacienteViewModel detalhesPacienteVM = new DetalhesPacienteViewModel(p.Id, p.Nome, p.Telefone, p.NumCartaoSUS, p.CPF);

                Pacientes.Add(detalhesPacienteVM);
            }
        }

        public VisualizarPacientesViewModel(string nome, string telefone, string numCartaoSUS, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            NumCartaoSUS = numCartaoSUS;
            CPF = cpf;
        }
    }

    public class DetalhesPacienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string NumCartaoSUS { get; set; }
        public string CPF { get; set; }

        public DetalhesPacienteViewModel(Guid id, string nome, string telefone, string numCartaoSUS, string cpf)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            NumCartaoSUS = numCartaoSUS;
            CPF = cpf;
        }
    }

    public class CadastrarPacienteViewModel
    {
        [Required(ErrorMessage = "O nome do paciente é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do paciente deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do paciente é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O número do cartão SUS é um campo obrigatório")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "O nº do cartão SUS deve conter 15 dígitos")]
        public string NumCartaoSUS { get; set; }

        [Required(ErrorMessage = "O CPF do paciente é um campo obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string CPF { get; set; }

        public CadastrarPacienteViewModel() { }

        public CadastrarPacienteViewModel(string nome, string telefone, string numCartaoSUS, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            NumCartaoSUS = numCartaoSUS;
            CPF = cpf;
        }
    }

    public class EditarPacienteViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do paciente é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do paciente deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do paciente é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O número do cartão SUS é um campo obrigatório")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "O nº do cartão SUS deve conter 15 dígitos")]
        public string NumCartaoSUS { get; set; }

        [Required(ErrorMessage = "O CPF do paciente é um campo obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string CPF { get; set; }

        public EditarPacienteViewModel() { }

        public EditarPacienteViewModel(Guid id, string nome, string telefone, string numCartaoSUS, string cpf)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            NumCartaoSUS = numCartaoSUS;
            CPF = cpf;
        }
    }

    public class ExcluirPacienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirPacienteViewModel() { }

        public ExcluirPacienteViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
