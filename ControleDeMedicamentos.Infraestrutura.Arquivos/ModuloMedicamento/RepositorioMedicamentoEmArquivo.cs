using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento
{
    public class RepositorioMedicamentoEmArquivo : RepositorioBase<Medicamento>
    {
        public RepositorioMedicamentoEmArquivo(ContextoDados contextoDados) : base(contextoDados) { }

        public override void Editar(Guid idParaAtualizar, Medicamento medicamentoAtualizado)
        {
            Medicamento medicamento = ObterRegistroPorID(idParaAtualizar);

            medicamento.Nome = medicamentoAtualizado.Nome;
            medicamento.Descricao = medicamentoAtualizado.Descricao;
            medicamento.QtdEstoque = medicamentoAtualizado.QtdEstoque;
            medicamento.Fornecedor = medicamentoAtualizado.Fornecedor;

            contextoDados.Salvar();
        }

        public override List<Medicamento> ObterRegistros()
        {
            return contextoDados.Medicamentos;
        }

        public override bool RegistroDuplicado(Medicamento medicamento)
        {
            foreach (Medicamento m in listaRegistros)
                if (m.Nome == medicamento.Nome && m.Id != medicamento.Id)
                    return true;
            return false;
        }
    }
}
