using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarPrescricoesViewModel
    {
        public List<DetalhesPrescricaoViewModel> Prescricoes { get; }

        public VisualizarPrescricoesViewModel(List<Prescricao> prescricoes)
        {
            Prescricoes = new List<DetalhesPrescricaoViewModel>();

            if (prescricoes is not null)
                Prescricoes = prescricoes.Select(p => new DetalhesPrescricaoViewModel(p.Id, p.Descricao, p.Paciente, p.DataEmissao, p.DataValidade, p.CrmMedico, p.MedicamentosPrescritos)).ToList();
        }
    }

    public class DetalhesPrescricaoViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CrmMedico { get; set; }
        public List<DetalhesMedicamentoPrescritoViewModel> MedicamentosPrescritos { get; set; } = new List<DetalhesMedicamentoPrescritoViewModel>();

        public DetalhesPrescricaoViewModel(Guid id, string descricao, Paciente paciente, DateTime dataEmissao, DateTime dataValidade, string crmMedico, List<MedicamentoPrescrito> medicamentosPrescritos)
        {
            Id = id;
            Descricao = descricao;
            Paciente = paciente;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
            CrmMedico = crmMedico;

            MedicamentosPrescritos = medicamentosPrescritos.Select(m => new DetalhesMedicamentoPrescritoViewModel(m.Id, m.Medicamento.Id, m.Medicamento, m.Dosagem, m.Periodo, m.Quantidade)).ToList();
        }
    }

    public class CadastrarPrescricaoViewModel
    {
        [Required(ErrorMessage = "A descrição da prescrição é um campo obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "A descrição do medicamento deve conter entre 5 e 255 caracteres")]
        public string Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataValidade { get; set; }

        [Required(ErrorMessage = "O CRM do médico é um campo obrigatório")]
        [RegularExpression(@"^\d{4,7}-?[A-Z]{2}$", ErrorMessage = "O CRM do médico deve seguir o padrão 1111000-UF")]
        public string CrmMedico { get; set; }

        [Required(ErrorMessage = "O paciente é um campo obrigatório")]
        public Guid PacienteID { get; set; }

        public List<SelectListItem> PacientesDisponiveis { get; set; } = new List<SelectListItem>();

        public CadastrarPrescricaoViewModel() { }

        public CadastrarPrescricaoViewModel(List<Paciente> pacientes)
        {
            PacientesDisponiveis = pacientes.Select(p => new SelectListItem(p.Nome, p.Id.ToString())).ToList();
        }
    }

    public class EditarPrescricaoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A descrição da prescrição é um campo obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "A descrição do medicamento deve conter entre 5 e 255 caracteres")]
        public string Descricao { get; set; }
        public DateTime DataValidade { get; set; }

        [Required(ErrorMessage = "O CRM do médico é um campo obrigatório")]
        [RegularExpression(@"^\d{4,7}-?[A-Z]{2}$", ErrorMessage = "O CRM do médico deve seguir o padrão 1111000-UF")]
        public string CrmMedico { get; set; }

        [Required(ErrorMessage = "O paciente é um campo obrigatório")]
        public Guid PacienteID { get; set; }

        public List<SelectListItem> PacientesDisponiveis { get; set; }

        public EditarPrescricaoViewModel()
        {
            PacientesDisponiveis = new List<SelectListItem>();
        }

        public EditarPrescricaoViewModel(Guid id, string descricao, DateTime dataValidade, string crmMedico, List<Paciente> pacientes) : this()
        {
            Id = id;
            Descricao = descricao;
            DataValidade = dataValidade;
            CrmMedico = crmMedico;
            PacientesDisponiveis = pacientes.Select(p => new SelectListItem(p.Nome, p.Id.ToString())).ToList();
        }
    }

    public class ExcluirPrescricaoViewModel
    {
        public Guid Id { get; set; }
        public Paciente Paciente { get; set; }

        public ExcluirPrescricaoViewModel() { }

        public ExcluirPrescricaoViewModel(Guid id, Paciente paciente)
        {
            Id = id;
            Paciente = paciente;
        }
    }

    public class GerenciarMedicamentosPrescritosViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string CrmMedico { get; set; }
        public Guid PacienteID { get; set; }
        public Paciente Paciente { get; set; }

        public List<SelectListItem> MedicamentosDisponiveis { get; set; }
        public List<DetalhesMedicamentoPrescritoViewModel> MedicamentosPrescritos { get; set; } = new List<DetalhesMedicamentoPrescritoViewModel>();

        public GerenciarMedicamentosPrescritosViewModel() { }

        public GerenciarMedicamentosPrescritosViewModel(Guid id, string descricao, string crmMedico, Guid pacienteID, Paciente paciente, List<Medicamento> medicamentos)
        {
            Id = id;
            Descricao = descricao;
            CrmMedico = crmMedico;
            PacienteID = pacienteID;
            Paciente = paciente;

            MedicamentosDisponiveis = medicamentos.Select(m => new SelectListItem(m.Nome, m.Id.ToString())).ToList();
        }

        public GerenciarMedicamentosPrescritosViewModel(Guid id, string descricao, string crmMedico, Guid pacienteID, Paciente paciente, List<Medicamento> medicamentos, List<MedicamentoPrescrito> medicamentosPrescritos) : this(id, descricao, crmMedico, pacienteID, paciente, medicamentos)
        {
            MedicamentosPrescritos = medicamentosPrescritos.Select(m => new DetalhesMedicamentoPrescritoViewModel(m.Id, m.Medicamento.Id, m.Medicamento, m.Dosagem, m.Periodo, m.Quantidade)).ToList();
        }
    }

    public class DetalhesMedicamentoPrescritoViewModel
    {
        public Guid Id { get; set; }
        public Guid MedicamentoID { get; set; }
        public Medicamento Medicamento { get; set; }
        public string Dosagem { get; set; }
        public string Periodo { get; set; }
        public int Quantidade { get; set; }

        public DetalhesMedicamentoPrescritoViewModel() { }

        public DetalhesMedicamentoPrescritoViewModel(Guid id, Guid medicamentoID, Medicamento medicamento, string dosagem, string periodo, int quantidade) : this()
        {
            Id = id;
            MedicamentoID = medicamentoID;
            Medicamento = medicamento;
            Dosagem = dosagem;
            Periodo = periodo;
            Quantidade = quantidade;
        }
    }

    public class AdicionarMedicamentoPrescritoViewModel
    {
        public Guid MedicamentoID { get; set; }
        public string DosagemMedicamento { get; set; }
        public string PeriodoMedicamento { get; set; }
        public int QuantidadeMedicamento { get; set; }
    }
}
