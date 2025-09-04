using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;

namespace ControleDeMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int QtdEstoque
        {
            get
            {
                int qtdEstoque = 0;

                foreach (EntradaMedicamento reqEntrada in Entradas)
                    if (reqEntrada.Medicamento.Id == Id)
                        qtdEstoque += reqEntrada.QtdEntrada;

                return qtdEstoque;
            }
        }

        public List<EntradaMedicamento> Entradas { get; set; } = new List<EntradaMedicamento>();

        public Medicamento() { }

        public Medicamento(string nome, string descricao, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            Fornecedor = fornecedor;
        }
    }
}
