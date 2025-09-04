using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor
{
    public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>
    {
        public RepositorioFornecedorEmArquivo(ContextoDados contextoDados) : base(contextoDados) { }

        public override void Editar(Guid idParaAtualizar, Fornecedor fornecedorAtualizado)
        {
            Fornecedor fornecedor = ObterRegistroPorID(idParaAtualizar);

            fornecedor.Nome = fornecedorAtualizado.Nome;
            fornecedor.Telefone = fornecedorAtualizado.Telefone;
            fornecedor.CNPJ = fornecedorAtualizado.CNPJ;

            contextoDados.Salvar();
        }

        public override List<Fornecedor> ObterRegistros()
        {
            return contextoDados.Fornecedores;
        }

        public override bool RegistroDuplicado(Fornecedor fornecedor)
        {
            foreach (Fornecedor f in listaRegistros)
                if (f.CNPJ == fornecedor.CNPJ && f.Id != fornecedor.Id)
                    return true;
            return false;
        }
    }
}
