namespace ControleDeMedicamentos.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public EntidadeBase(Guid id)
        {
            Id = id;
        }
    }
}
