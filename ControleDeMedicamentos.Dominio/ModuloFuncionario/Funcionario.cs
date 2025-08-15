using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public Funcionario(Guid id, string nome, string telefone, string cpf) : base(id)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
    }
}
