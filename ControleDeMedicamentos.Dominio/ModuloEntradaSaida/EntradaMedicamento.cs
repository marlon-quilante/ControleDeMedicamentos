using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;

namespace ControleDeMedicamentos.Dominio.ModuloEntradaSaida
{
    public class EntradaMedicamento
    {
        public Guid Id { get; set; }
        public Medicamento Medicamento { get; set; }
        public Funcionario Funcionario { get; set; }
        public int QtdEntrada { get; set; }
        public DateTime DataEntrada { get; set; }

        public EntradaMedicamento() { }

        public EntradaMedicamento(Medicamento medicamento, Funcionario funcionario, int qtdEntrada)
        {
            Id = Guid.NewGuid();
            DataEntrada = DateTime.Now;
            Funcionario = funcionario;
            Medicamento = medicamento;
            QtdEntrada = qtdEntrada;
        }
    }
}
