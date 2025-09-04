using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarEntradasMedicamentoViewModel
    {
        public List<DetalhesEntradaMedicamentoViewModel> EntradasMedicamento { get; set; }

        public VisualizarEntradasMedicamentoViewModel(List<EntradaMedicamento> entradasMedicamento)
        {
            if (entradasMedicamento is not null)
                EntradasMedicamento = entradasMedicamento.Select(x => new DetalhesEntradaMedicamentoViewModel(x.Id, x.Medicamento, x.Funcionario, x.QtdEntrada, x.DataEntrada)).ToList();
        }
    }

    public class DetalhesEntradaMedicamentoViewModel
    {
        public Guid Id { get; set; }
        public Medicamento Medicamento { get; set; }
        public Funcionario Funcionario { get; set; }
        public int QtdEntrada { get; set; }
        public DateTime DataEntrada { get; set; }

        public DetalhesEntradaMedicamentoViewModel(Guid id, Medicamento medicamento, Funcionario funcionario, int qtdEntrada, DateTime dataEntrada)
        {
            Id = id;
            Medicamento = medicamento;
            Funcionario = funcionario;
            QtdEntrada = qtdEntrada;
            DataEntrada = dataEntrada;
        }
    }

    public class CadastrarEntradaMedicamentoViewModel
    {
        [Required(ErrorMessage = "O medicamento é um campo obrigatório")]
        public Guid MedicamentoID { get; set; }
        
        public List<SelectListItem> MedicamentosDisponiveis { get; set; }

        [Required(ErrorMessage = "O funcionário é um campo obrigatório")]
        public Guid FuncionarioID { get; set; }
        
        public List<SelectListItem> FuncionariosDisponiveis { get; set; }

        [Required(ErrorMessage = "A quantidade de entrada é um campo obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade de entrada deve ser um valor positivo")]
        public int QtdEntrada { get; set; }

        public CadastrarEntradaMedicamentoViewModel() 
        {
            MedicamentosDisponiveis = new List<SelectListItem>();
            FuncionariosDisponiveis = new List<SelectListItem>();
        }

        public CadastrarEntradaMedicamentoViewModel(List<Medicamento> medicamentos, List<Funcionario> funcionarios)
        {
            MedicamentosDisponiveis = medicamentos.Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
            FuncionariosDisponiveis = funcionarios.Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
        }
    }
}
