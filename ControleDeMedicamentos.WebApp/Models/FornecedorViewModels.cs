using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarFornecedoresViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        public List<DetalhesFornecedorViewModel> Fornecedores { get; set; }

        public VisualizarFornecedoresViewModel(List<Fornecedor> fornecedores)
        {
            Fornecedores = new List<DetalhesFornecedorViewModel>();

            foreach (Fornecedor f in fornecedores)
            {
                DetalhesFornecedorViewModel detalhesVM = new DetalhesFornecedorViewModel(f.Id, f.Nome, f.Telefone, f.CNPJ);

                Fornecedores.Add(detalhesVM);
            }
        }

        public VisualizarFornecedoresViewModel(string nome, string telefone, string cnpj)
        {
            Nome = nome;
            Telefone = telefone;
            CNPJ = cnpj;
        }
    }

    public class DetalhesFornecedorViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }

        public DetalhesFornecedorViewModel(Guid id, string nome, string telefone, string cNPJ)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CNPJ = cNPJ;
        }
    }

    public class CadastrarFornecedorViewModel 
    {
        [Required(ErrorMessage = "O nome do fornecedor é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do fornecedor deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do fornecedor é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CNPJ do fornecedor é um campo obrigatório")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$", ErrorMessage = "O CNPJ deve estar no formato 99.999.999\\9999-99")]
        public string CNPJ { get; set; }

        public CadastrarFornecedorViewModel() { }

        public CadastrarFornecedorViewModel(string nome, string telefone, string cnpj)
        {
            Nome = nome;
            Telefone = telefone;
            CNPJ = cnpj;
        }
    }

    public class EditarFornecedorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do fornecedor é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do fornecedor deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone do fornecedor é um campo obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s{1}\d{5}\-\d{4}$", ErrorMessage = "O telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CNPJ do fornecedor é um campo obrigatório")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$", ErrorMessage = "O CNPJ deve estar no formato 99.999.999\\9999-99")]
        public string CNPJ { get; set; }

        public EditarFornecedorViewModel() { }

        public EditarFornecedorViewModel(Guid id, string nome, string telefone, string cnpj)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CNPJ = cnpj;
        }
    }

    public class ExcluirFornecedorViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirFornecedorViewModel() { }

        public ExcluirFornecedorViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
