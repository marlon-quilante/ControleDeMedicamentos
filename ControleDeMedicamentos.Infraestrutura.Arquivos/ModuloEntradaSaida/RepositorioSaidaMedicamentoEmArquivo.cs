using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida
{
    public class RepositorioSaidaMedicamentoEmArquivo
    {
        private readonly ContextoDados contextoDados;
        private readonly List<SaidaMedicamento> listaSaidas = new List<SaidaMedicamento>();
        private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

        public RepositorioSaidaMedicamentoEmArquivo(ContextoDados contextoDados)
        {
            this.contextoDados = contextoDados;
            listaSaidas = ObterRegistros();
            repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        }

        public void Cadastrar(SaidaMedicamento novaSaida)
        {
            if (novaSaida != null)
            {
                listaSaidas.Add(novaSaida);
                repositorioMedicamento.SaidaMedicamento(novaSaida);
                contextoDados.Salvar();
            }
        }

        public List<SaidaMedicamento> ObterRegistros()
        {
            return contextoDados.SaidasMedicamento;
        }
    }
}
