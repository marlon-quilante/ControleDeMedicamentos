using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFornecedor
{
    public class RepositorioFornecedorEmSql
    {
        private readonly IDbConnection connection;

        public RepositorioFornecedorEmSql(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Cadastrar(Fornecedor novoFornecedor)
        {
            const string sql = @"INSERT INTO TBFornecedor (Id, Nome, Telefone, CNPJ) VALUES (@Id, @Nome, @Telefone, @CNPJ)";

            connection.Execute(sql, new
            {
                Id = Guid.NewGuid(),
                Nome = novoFornecedor.Nome,
                Telefone = novoFornecedor.Telefone,
                CNPJ = novoFornecedor.CNPJ
            });
        }

        public void Editar(Guid idParaAtualizar, Fornecedor fornecedorAtualizado)
        {
            const string sql = @"UPDATE TBFornecedor SET Nome = @Nome, Telefone = @Telefone, CNPJ = @CNPJ WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaAtualizar,
                fornecedorAtualizado.Nome,
                fornecedorAtualizado.Telefone,
                fornecedorAtualizado.CNPJ
            });
        }

        public void Excluir(Guid idParaExcluir)
        {
            const string sql = @"DELETE FROM TBFornecedor WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaExcluir
            });
        }

        public List<Fornecedor> ObterRegistros()
        {
            const string sql = @"SELECT * FROM TBFornecedor ORDER BY Nome";

            return connection.Query<Fornecedor>(sql).ToList();
        }

        public Fornecedor ObterRegistroPorID(Guid id)
        {
            const string sql = @"SELECT * FROM TBFornecedor WHERE Id = @Id";

            return connection.QueryFirstOrDefault<Fornecedor>(sql, new { Id = id });
        }

        public bool RegistroDuplicado(Fornecedor fornecedor)
        {
            const string sql = @"SELECT * FROM TBFornecedor";

            List<Fornecedor> listaRegistros = connection.Query<Fornecedor>(sql).ToList();

            foreach (Fornecedor f in listaRegistros)
                if (f.CNPJ == fornecedor.CNPJ && f.Id != fornecedor.Id)
                    return true;
            return false;
        }
    }
}