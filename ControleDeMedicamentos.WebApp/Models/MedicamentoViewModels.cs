using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarMedicamentosViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QtdEstoque { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<DetalhesMedicamentoViewModel> Medicamentos { get; set; }

        public VisualizarMedicamentosViewModel(List<Medicamento> medicamentos)
        {
            Medicamentos = new List<DetalhesMedicamentoViewModel>();

            foreach (Medicamento m in medicamentos)
            {
                DetalhesMedicamentoViewModel detalhesVM = new DetalhesMedicamentoViewModel(m.Id, m.Nome, m.Descricao, m.QtdEstoque, m.Fornecedor);

                Medicamentos.Add(detalhesVM);
            }
        }

        public VisualizarMedicamentosViewModel(string nome, string descricao, int qtdEstoque, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            QtdEstoque = qtdEstoque;
            Fornecedor = fornecedor;
        }
    }

    public class DetalhesMedicamentoViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QtdEstoque { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public DetalhesMedicamentoViewModel(Guid id, string nome, string descricao, int qtdEstoque, Fornecedor fornecedor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            QtdEstoque = qtdEstoque;
            Fornecedor = fornecedor;
        }
    }

    public class CadastrarMedicamentoViewModel
    {
        [Required(ErrorMessage = "O nome do medicamento é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do medicamento deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome do medicamento é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do medicamento deve conter entre 3 e 100 caracteres")]
        public string Descricao { get; set; }

        [MinLength(0, ErrorMessage = "A qtd. em estoque do produto deve ser maior que 0")]
        public int QtdEstoque { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Required(ErrorMessage = "O medicamento precisa estar vinculado a um fornecedor")]
        public Guid FornecedorID { get; set; }
        public List<SelectListItem> FornecedoresDisponiveis { get; set; }

        public CadastrarMedicamentoViewModel() 
        {
            FornecedoresDisponiveis = new List<SelectListItem>();
        }

        public CadastrarMedicamentoViewModel(List<Fornecedor> fornecedores) : this()
        {   
            foreach (Fornecedor f in fornecedores)
            {
                SelectListItem fornecedorDisponivel = new SelectListItem(f.Nome.ToString(), f.Id.ToString());

                FornecedoresDisponiveis.Add(fornecedorDisponivel);
            }
        }
    }

    public class EditarMedicamentoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do medicamento é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do medicamento deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome do medicamento é um campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do medicamento deve conter entre 3 e 100 caracteres")]
        public string Descricao { get; set; }

        public int QtdEstoque { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Required(ErrorMessage = "O medicamento precisa estar vinculado a um fornecedor")]
        public Guid FornecedorID { get; set; }
        public List<SelectListItem> FornecedoresDisponiveis { get; set; }

        public EditarMedicamentoViewModel() { }

        public EditarMedicamentoViewModel(Guid id, string nome, string descricao, int qtdEstoque, List<Fornecedor> fornecedores)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            QtdEstoque = qtdEstoque;

            foreach (Fornecedor f in fornecedores)
            {
                SelectListItem fornecedor = new SelectListItem();

                FornecedoresDisponiveis.Add(fornecedor);
            }
        }
    }

    public class ExcluirMedicamentoViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirMedicamentoViewModel() { }

        public ExcluirMedicamentoViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
