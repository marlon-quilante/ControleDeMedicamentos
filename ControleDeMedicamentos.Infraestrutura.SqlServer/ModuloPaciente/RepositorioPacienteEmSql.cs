using System.Data;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using Dapper;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente
{
    public class RepositorioPacienteEmSql
    {
        private readonly IDbConnection connection;

        public RepositorioPacienteEmSql(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Cadastrar(Paciente novoPaciente)
        {
            const string sql = @"INSERT INTO TBPaciente (Id, Nome, Telefone, NumCartaoSUS, CPF) VALUES (@Id, @Nome, @Telefone, @NumCartaoSus, @CPF)";

            connection.Execute(sql, new
            {
                Id = Guid.NewGuid(),
                Nome = novoPaciente.Nome,
                Telefone = novoPaciente.Telefone,
                NumCartaoSUS = novoPaciente.NumCartaoSUS,
                CPF = novoPaciente.CPF
            });
        }

        public void Editar(Guid idParaAtualizar, Paciente pacienteAtualizado)
        {
            const string sql = @"UPDATE TBPaciente SET Nome = @Nome, Telefone = @Telefone, NumCartaoSUS = @NumCartaoSus, CPF = @CPF WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaAtualizar,
                pacienteAtualizado.Nome,
                pacienteAtualizado.Telefone,
                pacienteAtualizado.NumCartaoSUS,
                pacienteAtualizado.CPF
            });
        }

        public void Excluir(Guid idParaExcluir)
        {
            const string sql = @"DELETE FROM TBPaciente WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaExcluir
            });
        }

        public List<Paciente> ObterRegistros()
        {
            const string sql = @"SELECT * FROM TBPaciente ORDER BY Nome";

            return connection.Query<Paciente>(sql).ToList();
        }

        public Paciente ObterRegistroPorID(Guid id)
        {
            const string sql = @"SELECT * FROM TBPaciente WHERE Id = @Id";

            return connection.QueryFirstOrDefault<Paciente>(sql, new { Id = id });
        }

        public Paciente ObterRegistroPorCPF(string cpf)
        {
            const string sql = @"SELECT * FROM TBPaciente WHERE CPF = @CPF";

            return connection.QueryFirstOrDefault<Paciente>(sql, new { CPF = cpf });
        }

        public bool RegistroDuplicado(Paciente paciente)
        {
            const string sql = @"SELECT * FROM TBPaciente";

            List<Paciente> listaRegistros = connection.Query<Paciente>(sql).ToList();

            foreach (Paciente p in listaRegistros)
                if (p.NumCartaoSUS == paciente.NumCartaoSUS && p.Id != paciente.Id)
                    return true;
            return false;
        }
    }
}