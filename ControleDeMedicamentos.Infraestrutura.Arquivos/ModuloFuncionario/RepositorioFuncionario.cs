using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario
{
    public class RepositorioFuncionario : RepositorioBase<Funcionario>
    {
        public RepositorioFuncionario(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override List<Funcionario> ObterRegistros()
        {
            return contextoDados.Funcionarios;
        }

        public override void Editar(Guid idParaAtualizar, Funcionario funcionarioAtualizado)
        {
            Funcionario funcionario = ObterRegistroPorID(idParaAtualizar);

            funcionario.Nome = funcionarioAtualizado.Nome;
            funcionario.Telefone = funcionarioAtualizado.Telefone;
            funcionario.CPF = funcionarioAtualizado.CPF;
        }
    }
}
