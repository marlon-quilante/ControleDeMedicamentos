using ControleDeMedicamentos.Dominio.ModuloMedicamento;

namespace ControleDeMedicamentos.Dominio.ModuloPrescricao
{
    public class MedicamentoPrescrito 
    {
        public Guid Id { get; set; }
        public Medicamento Medicamento { get; set; }
        public Prescricao Prescricao { get; set; }
        public string Dosagem { get; set; }
        public string Periodo { get; set; }
        public int Quantidade { get; set; }

        public MedicamentoPrescrito() { }

        public MedicamentoPrescrito(Medicamento medicamento, Prescricao prescricao, string dosagem, string periodo, int quantidade)
        {
            Id = Guid.NewGuid();
            Medicamento = medicamento;
            Dosagem = dosagem;
            Periodo = periodo;
            Quantidade = quantidade;
            Prescricao = prescricao;
        }
    }
}
