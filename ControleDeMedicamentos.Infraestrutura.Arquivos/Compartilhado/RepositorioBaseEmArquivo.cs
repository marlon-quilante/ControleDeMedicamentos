using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado
{
    public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
    {
        public ContextoDados contextoDados;
        public List<T> listaRegistros = new List<T>();

        public RepositorioBaseEmArquivo(ContextoDados contextoDados)
        {
            this.contextoDados = contextoDados;
            listaRegistros = ObterRegistros();
        }

        public void Cadastrar(T novoRegistro)
        {
            if (novoRegistro != null)
            {
                listaRegistros.Add(novoRegistro);
                novoRegistro.Id = Guid.NewGuid();
                contextoDados.Salvar();
            }
        }

        public abstract void Editar(Guid idParaAtualizar, T registroAtualizado);

        public void Excluir(Guid id)
        {
            T registro = ObterRegistroPorID(id);
            listaRegistros.Remove(registro);
            contextoDados.Salvar();
        }

        public abstract List<T> ObterRegistros();

        public T ObterRegistroPorID(Guid id)
        {
            var registro = listaRegistros.Find(r => r.Id == id);

            return registro;
        }

        public abstract bool RegistroDuplicado(T registro);
    }
}
