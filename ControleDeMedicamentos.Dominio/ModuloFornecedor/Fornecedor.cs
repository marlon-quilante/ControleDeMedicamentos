using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }

        public Fornecedor() { }

        public Fornecedor(string nome, string telefone, string cnpj)
        {
            Nome = nome;
            Telefone = telefone;
            CNPJ = cnpj;
        }
    }
}
