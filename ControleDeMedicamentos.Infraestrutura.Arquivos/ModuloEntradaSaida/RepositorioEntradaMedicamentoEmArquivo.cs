using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloEntradaSaida
{
    public class RepositorioEntradaMedicamentoEmArquivo
    {
        private readonly ContextoDados contextoDados;
        private readonly List<EntradaMedicamento> listaEntradas = new List<EntradaMedicamento>();
        private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

        public RepositorioEntradaMedicamentoEmArquivo(ContextoDados contextoDados)
        {
            this.contextoDados = contextoDados;
            listaEntradas = ObterRegistros();
            repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        }

        public void Cadastrar(EntradaMedicamento novaEntrada)
        {
            if (novaEntrada != null)
            {
                listaEntradas.Add(novaEntrada);
                repositorioMedicamento.EntradaMedicamento(novaEntrada);
                contextoDados.Salvar();
            }
        }

        public List<EntradaMedicamento> ObterRegistros()
        {
            return contextoDados.EntradasMedicamento;
        }
    }
}
