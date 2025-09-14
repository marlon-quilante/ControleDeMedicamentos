using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models
{
    public class VisualizarEntradasSaidasMedicamentoViewModel
    {
        public List<DetalhesEntradaMedicamentoViewModel> EntradasMedicamento { get; set; }
        public List<DetalhesSaidaMedicamentoViewModel> SaidasMedicamento { get; set; }

        public VisualizarEntradasSaidasMedicamentoViewModel(List<EntradaMedicamento> entradasMedicamento, List<SaidaMedicamento> saidasMedicamento)
        {
            if (entradasMedicamento is not null)
                EntradasMedicamento = entradasMedicamento.Select(x => new DetalhesEntradaMedicamentoViewModel(x.Id, x.Medicamento.Nome, x.Funcionario.Nome, x.QtdEntrada, x.DataEntrada)).ToList();

            if (saidasMedicamento != null)
                SaidasMedicamento = saidasMedicamento.Select(x => new DetalhesSaidaMedicamentoViewModel(x.Id, x.Funcionario.Nome, x.Prescricao.Paciente.Nome, x.Prescricao.Descricao, x.DataSaida, x.Prescricao.MedicamentosPrescritos)).ToList();
        }
    }

    public class DetalhesEntradaMedicamentoViewModel
    {
        public Guid Id { get; set; }
        public string NomeMedicamento { get; set; }
        public string NomeFuncionario { get; set; }
        public int QtdEntrada { get; set; }
        public DateTime DataEntrada { get; set; }

        public DetalhesEntradaMedicamentoViewModel(Guid id, string nomeMedicamento, string nomeFuncionario, int qtdEntrada, DateTime dataEntrada)
        {
            Id = id;
            NomeMedicamento = nomeMedicamento;
            NomeFuncionario = nomeFuncionario;
            QtdEntrada = qtdEntrada;
            DataEntrada = dataEntrada;
        }
    }

    public class RegistrarEntradaMedicamentoViewModel
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

        public RegistrarEntradaMedicamentoViewModel() 
        {
            MedicamentosDisponiveis = new List<SelectListItem>();
            FuncionariosDisponiveis = new List<SelectListItem>();
        }

        public RegistrarEntradaMedicamentoViewModel(List<Medicamento> medicamentos, List<Funcionario> funcionarios)
        {
            MedicamentosDisponiveis = medicamentos.Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
            FuncionariosDisponiveis = funcionarios.Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
        }
    }

    public class DetalhesSaidaMedicamentoViewModel 
    {
        public Guid Id { get; set; }
        public string NomeFuncionario { get; set; }
        public string NomePaciente { get; set; }
        public string DescricaoPrescricao { get; set; }
        public DateTime DataSaida { get; set; }
        public List<DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel> MedicamentosPrescritos { get; set; }

        public DetalhesSaidaMedicamentoViewModel()
        {
            MedicamentosPrescritos = new List<DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel>();
        }

        public DetalhesSaidaMedicamentoViewModel(Guid id, string nomeFuncionario, string nomePaciente, string descricaoPrescricao, DateTime dataSaida, List<MedicamentoPrescrito> medicamentoPrescritos)
        {
            Id = id;
            NomeFuncionario = nomeFuncionario;
            NomePaciente = nomePaciente;
            DescricaoPrescricao = descricaoPrescricao;
            DataSaida = dataSaida;
            MedicamentosPrescritos = medicamentoPrescritos.Select(x => new DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel(x.Medicamento.Nome, x.Dosagem, x.Periodo, x.Quantidade)).ToList();
        }
    }

    public class DadosIniciaisSaidaMedicamentoViewModel
    {
        public List<SelectListItem> FuncionariosDisponiveis { get; set; } = new List<SelectListItem>();
        [Required(ErrorMessage = "O funcionário é um campo obrigatório")]
        public Guid FuncionarioID { get; set; }

        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 999.999.999-99")]
        [Required(ErrorMessage = "O CPF do paciente é um campo obrigatório")]
        public string CPFPaciente { get; set; }
        
        public DadosIniciaisSaidaMedicamentoViewModel() { }

        public DadosIniciaisSaidaMedicamentoViewModel(List<Funcionario> funcionarios) : this()
        {
            FuncionariosDisponiveis = funcionarios.Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
        }

        public DadosIniciaisSaidaMedicamentoViewModel(string cpfPaciente, Guid funcionarioID)
        {
            CPFPaciente = cpfPaciente;
            FuncionarioID = funcionarioID;
        }
    }

    public class DetalhesPrescricaoSaidaMedicamentoViewModel
    {
        public Guid Id { get; }
        public string Descricao { get; }
        public DateTime DataValidade { get; }

        public DetalhesPrescricaoSaidaMedicamentoViewModel(Guid id, string descricao, DateTime dataValidade) 
        {
            Id = id;
            Descricao = descricao;
            DataValidade = dataValidade;
        }
    }

    public class PrescricoesSaidaMedicamentoViewModel 
    {
        public List<DetalhesPrescricaoSaidaMedicamentoViewModel> Prescricoes { get; set; }
        public Guid PrescricaoID { get; set; }
        public string NomeFuncionario { get; }
        public Guid FuncionarioID { get; }
        public string NomePaciente { get; }
        public Guid PacienteID { get; }
        public string TelefonePaciente { get; }

        public PrescricoesSaidaMedicamentoViewModel()
        {
            Prescricoes = new List<DetalhesPrescricaoSaidaMedicamentoViewModel>();
        }

        public PrescricoesSaidaMedicamentoViewModel(string nomeFuncionario, Guid funcionarioID, string nomePaciente, Guid pacienteID, string telefonePaciente, List<Prescricao> prescricoes)
        {
            NomeFuncionario = nomeFuncionario;
            FuncionarioID = funcionarioID;
            NomePaciente = nomePaciente;
            PacienteID = pacienteID;
            TelefonePaciente = telefonePaciente;
            Prescricoes = prescricoes.Select(x => new DetalhesPrescricaoSaidaMedicamentoViewModel(x.Id, x.Descricao, x.DataValidade)).ToList();
        }

        public PrescricoesSaidaMedicamentoViewModel(Guid prescricaoID)
        {
            PrescricaoID = prescricaoID;
        }
    }

    public class DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel
    {
        public string NomeMedicamento { get; }
        public string Dosagem { get; }
        public string Periodo { get; }
        public int Quantidade { get; }

        public DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel(string nomeMedicamento, string dosagem, string periodo, int quantidade)
        {
            NomeMedicamento = nomeMedicamento;
            Dosagem = dosagem;
            Periodo = periodo;
            Quantidade = quantidade;
        }
    }

    public class RegistrarSaidaMedicamentoViewModel
    {
        public string NomeFuncionario { get; }
        public Guid FuncionarioID { get; }
        public string NomePaciente { get; }
        public Guid PacienteID { get; }
        public DateTime DataSaida { get; }
        public string DescricaoPrescricao { get; }
        public Guid PrescricaoID { get; }
        public List<DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel> MedicamentosPrescritos { get; }

        public RegistrarSaidaMedicamentoViewModel()
        {
            MedicamentosPrescritos = new List<DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel>();
        }

        public RegistrarSaidaMedicamentoViewModel(string nomeFuncionario, Guid funcionarioID, string nomePaciente, Guid pacienteID, string descricaoPrescricao, Guid prescricaoID, List<MedicamentoPrescrito> medicamentosPrescritos)
        {
            NomeFuncionario = nomeFuncionario;
            FuncionarioID = funcionarioID;
            NomePaciente = nomePaciente;
            PacienteID = pacienteID;
            DataSaida = DateTime.Now;
            DescricaoPrescricao = descricaoPrescricao;
            PrescricaoID = prescricaoID;
            MedicamentosPrescritos = medicamentosPrescritos.Select(x => new DetalhesMedicamentoPrescritoSaidaMedicamentoViewModel(x.Medicamento.Nome, x.Dosagem, x.Periodo, x.Quantidade)).ToList();
        }
    }
}
