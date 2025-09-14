using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao
{
    public class RepositorioPrescricaoEmArquivo : RepositorioBaseEmArquivo<Prescricao>
    {
        public RepositorioPrescricaoEmArquivo(ContextoDados contextoDados) : base(contextoDados) { }

        public override void Editar(Guid idParaAtualizar, Prescricao prescricaoAtualizada)
        {
            Prescricao prescricao = ObterRegistroPorID(idParaAtualizar);

            prescricao.Descricao = prescricaoAtualizada.Descricao;
            prescricao.Paciente = prescricaoAtualizada.Paciente;
            prescricao.DataValidade = prescricaoAtualizada.DataValidade;
            prescricao.CrmMedico = prescricaoAtualizada.CrmMedico;

            contextoDados.Salvar();
        }

        public override List<Prescricao> ObterRegistros()
        {
            return contextoDados.Prescricoes;
        }

        public List<Prescricao> ObterPrescricoesPorPaciente(Paciente paciente)
        {
            List<Prescricao> prescricoes = new List<Prescricao>();

            foreach (Prescricao p in listaRegistros)
                if (p.Paciente.Id == paciente.Id)
                    prescricoes.Add(p);

            return prescricoes;
        }

        public override bool RegistroDuplicado(Prescricao registro)
        {
            throw new NotImplementedException();
        }
    }
}
