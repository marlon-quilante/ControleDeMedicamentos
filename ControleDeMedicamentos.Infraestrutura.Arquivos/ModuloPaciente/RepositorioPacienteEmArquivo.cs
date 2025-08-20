using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente
{
    public class RepositorioPacienteEmArquivo : RepositorioBase<Paciente>
    {
        public RepositorioPacienteEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        public override void Editar(Guid idParaAtualizar, Paciente pacienteAtualizado)
        {
            Paciente paciente = ObterRegistroPorID(idParaAtualizar);

            paciente.Nome = pacienteAtualizado.Nome;
            paciente.NumCartaoSUS = pacienteAtualizado.NumCartaoSUS;
            paciente.Telefone = pacienteAtualizado.Telefone;
            paciente.CPF = pacienteAtualizado.CPF;

            contextoDados.Salvar();
        }

        public override List<Paciente> ObterRegistros()
        {
            return contextoDados.Pacientes;
        }

        public override bool RegistroDuplicado(Paciente paciente)
        {
            foreach (Paciente p in listaRegistros)
                if (p.NumCartaoSUS == paciente.NumCartaoSUS && p.Id != paciente.Id)
                    return true;
            return false;
        }
    }
}
