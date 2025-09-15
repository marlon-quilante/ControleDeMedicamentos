using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFuncionario
{
    public class RepositorioFuncionarioEmSql
    {
        private readonly IDbConnection connection;

        public RepositorioFuncionarioEmSql(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Cadastrar(Funcionario novoFuncionario)
        {
            const string sql = @"INSERT INTO TBFuncionario (Id, Nome, Telefone, CPF) VALUES (@Id, @Nome, @Telefone, @CPF)";

            connection.Execute(sql, new
            {
                Id = Guid.NewGuid(),
                Nome = novoFuncionario.Nome,
                Telefone = novoFuncionario.Telefone,
                CPF = novoFuncionario.CPF
            });
        }

        public void Editar(Guid idParaAtualizar, Funcionario funcionarioAtualizado)
        {
            const string sql = @"UPDATE TBFuncionario SET Nome = @Nome, Telefone = @Telefone, CPF = @CPF WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaAtualizar,
                funcionarioAtualizado.Nome,
                funcionarioAtualizado.Telefone,
                funcionarioAtualizado.CPF
            });
        }

        public void Excluir(Guid idParaExcluir)
        {
            const string sql = @"DELETE FROM TBFuncionario WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaExcluir
            });
        }

        public List<Funcionario> ObterRegistros()
        {
            const string sql = @"SELECT * FROM TBFuncionario ORDER BY Nome";

            return connection.Query<Funcionario>(sql).ToList();
        }

        public Funcionario ObterRegistroPorID(Guid id)
        {
            const string sql = @"SELECT * FROM TBFuncionario WHERE Id = @Id";

            return connection.QueryFirstOrDefault<Funcionario>(sql, new { Id = id });
        }

        public bool RegistroDuplicado(Funcionario funcionario)
        {
            const string sql = @"SELECT * FROM TBFuncionario";

            List<Funcionario> listaRegistros = connection.Query<Funcionario>(sql).ToList();

            foreach (Funcionario f in listaRegistros)
                if (f.CPF == funcionario.CPF && f.Id != funcionario.Id)
                    return true;
            return false;
        }
    }
}