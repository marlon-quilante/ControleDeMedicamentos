using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarFuncionariosViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public List<DetalhesFuncionarioViewModel> Funcionarios { get; set; }

        public VisualizarFuncionariosViewModel(List<Funcionario> funcionarios)
        {
            Funcionarios = new List<DetalhesFuncionarioViewModel>();

            foreach (var f in funcionarios)
            {
                DetalhesFuncionarioViewModel detalhesFuncionarioVM = new DetalhesFuncionarioViewModel(f.Id, f.Nome, f.Telefone, f.CPF);

                Funcionarios.Add(detalhesFuncionarioVM);
            }
        }

        public VisualizarFuncionariosViewModel(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }

    public class DetalhesFuncionarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public DetalhesFuncionarioViewModel(Guid id, string nome, string telefone, string cpf)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }

    public class CadastrarFuncionarioViewModel 
    {
        [Required(ErrorMessage = "O nome do funcionário é um campo obrigatório")]
        [StringLength(100, MinimumLength=3, ErrorMessage = "O nome do funcionário deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do funcionário é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF do funcionário é um campo obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string CPF { get; set; }

        public CadastrarFuncionarioViewModel() { }

        public CadastrarFuncionarioViewModel(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }

    public class EditarFuncionarioViewModel 
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do funcionário deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do funcionário é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF do funcionário é um campo obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        public string CPF { get; set; }

        public EditarFuncionarioViewModel() { }

        public EditarFuncionarioViewModel(Guid id, string nome, string telefone, string cpf)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }

    public class ExcluirFuncionarioViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirFuncionarioViewModel() { }

        public ExcluirFuncionarioViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
