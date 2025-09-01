using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloPaciente;

namespace ControleDeMedicamentos.Dominio.ModuloPrescricao
{
    public class Prescricao : EntidadeBase<Prescricao>
    {
        public string Descricao { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CrmMedico { get; set; } //Regex = @"^\d{4,7}-?[A-Z]{2}$"
        public List<MedicamentoPrescrito> MedicamentosPrescritos { get; set; } = new List<MedicamentoPrescrito>();

        public Prescricao()
        {
        }

        public Prescricao(string descricao, Paciente paciente, DateTime dataValidade, string crmMedico)
        {
            Descricao = descricao;
            Paciente = paciente;
            DataEmissao = DateTime.Now;
            DataValidade = dataValidade;
            CrmMedico = crmMedico;
        }

        public void AdicionarMedicamentoPrescrito(MedicamentoPrescrito medicamentoPrescrito)
        {
            MedicamentosPrescritos.Add(medicamentoPrescrito);
        }

        public void RemoverMedicamentoPrescrito(Guid idMedicamentoPrescrito)
        {
            MedicamentoPrescrito medicamentoPrescrito = MedicamentosPrescritos.Find(m => m.Id == idMedicamentoPrescrito);

            if (medicamentoPrescrito is null)
                return;

            MedicamentosPrescritos.Remove(medicamentoPrescrito);
        }
    }
}
