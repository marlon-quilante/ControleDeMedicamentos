using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;

namespace ControleDeMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<EntradaMedicamento> Entradas { get; set; } = new List<EntradaMedicamento>();
        public List<SaidaMedicamento> Saidas { get; set; } = new List<SaidaMedicamento>();
        public int QtdEstoque
        {
            get
            {
                int qtdEstoque = 0;

                foreach (EntradaMedicamento e in Entradas)
                    if (e.Medicamento.Id == Id)
                        qtdEstoque += e.QtdEntrada;

                foreach (SaidaMedicamento s in Saidas)
                    foreach (MedicamentoPrescrito mp in s.Prescricao.MedicamentosPrescritos)
                        if (mp.Medicamento.Id == Id)
                            qtdEstoque -= mp.Quantidade;

                return qtdEstoque;
            }
        }

        public Medicamento() { }

        public Medicamento(string nome, string descricao, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            Fornecedor = fornecedor;
        }
    }
}
