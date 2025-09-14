using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string NumCartaoSUS { get; set; }
        public string CPF { get; set; }
        
        public Paciente() { }

        public Paciente(string nome, string telefone, string numCartaoSUS, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            NumCartaoSUS = numCartaoSUS;
            CPF = cpf;
        }
    }
}
