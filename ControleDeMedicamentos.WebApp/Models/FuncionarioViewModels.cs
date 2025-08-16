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
        [Range(2, 100)]
        public string Nome { get; set; }
        public string Telefone { get; set; }
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
        [Range(2, 100)]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public EditarFuncionarioViewModel(Guid id, string nome, string telefone, string cpf)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }

    public class ExcluirFuncionarioViewModel { }
}
