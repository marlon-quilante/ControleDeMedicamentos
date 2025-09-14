using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;

namespace ControleDeMedicamentos.Dominio.ModuloEntradaSaida
{
    public class SaidaMedicamento
    {
        public Guid Id { get; set; }
        public DateTime DataSaida { get; set; }
        public Funcionario Funcionario { get; set; }
        public Prescricao Prescricao { get; set; }

        public SaidaMedicamento() { }

        public SaidaMedicamento(Prescricao prescricao, Funcionario funcionario)
        {
            Id = Guid.NewGuid();
            DataSaida = DateTime.Now;
            Prescricao = prescricao;
            Funcionario = funcionario;
        }
    }
}
