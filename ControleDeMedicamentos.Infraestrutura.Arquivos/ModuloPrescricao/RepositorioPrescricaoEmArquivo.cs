using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao
{
    public class RepositorioPrescricaoEmArquivo : RepositorioBase<Prescricao>
    {
        public RepositorioPrescricaoEmArquivo(ContextoDados contextoDados) : base(contextoDados) { }

        public override void Editar(Guid idParaAtualizar, Prescricao prescricaoAtualizada)
        {
            Prescricao prescricao = ObterRegistroPorID(idParaAtualizar);

            prescricao.Descricao = prescricaoAtualizada.Descricao;
            prescricao.Paciente = prescricaoAtualizada.Paciente;
            prescricao.DataValidade = prescricaoAtualizada.DataValidade;
            prescricao.CrmMedico = prescricaoAtualizada.CrmMedico;
        }

        public override List<Prescricao> ObterRegistros()
        {
            return contextoDados.Prescricoes;
        }

        public override bool RegistroDuplicado(Prescricao registro)
        {
            throw new NotImplementedException();
        }
    }
}
