using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;

namespace ControleDeMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QtdEstoque { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public Medicamento() { }

        public Medicamento(string nome, string descricao, int qtdEstoque, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            QtdEstoque = qtdEstoque;
            Fornecedor = fornecedor;
        }
    }
}
