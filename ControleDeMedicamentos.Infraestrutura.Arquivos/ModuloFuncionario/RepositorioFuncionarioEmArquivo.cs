using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario
{
    public class RepositorioFuncionarioEmArquivo : RepositorioBase<Funcionario>
    {
        public RepositorioFuncionarioEmArquivo(ContextoDados contextoDados) : base(contextoDados)
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

            contextoDados.Salvar();
        }

        public override bool RegistroDuplicado(Funcionario funcionario)
        {
            foreach (Funcionario f in listaRegistros)
                if (f.CPF == funcionario.CPF && f.Id != funcionario.Id)
                    return true;
            return false;
        }
    }
}
