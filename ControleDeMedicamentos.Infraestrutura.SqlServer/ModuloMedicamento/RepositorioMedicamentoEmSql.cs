using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento
{
    public class RepositorioMedicamentoEmSql(IDbConnection connection)
    {
        public void Cadastrar(Medicamento novoMedicamento)
        {
            const string sql = @"INSERT INTO TBMedicamento (Id, Nome, Descricao, FornecedorID, QtdEstoque) VALUES (@Id, @Nome, @Descricao, @FornecedorID, @QtdEstoque)";

            connection.Execute(sql, new
            {
                novoMedicamento.Id,
                novoMedicamento.Nome,
                novoMedicamento.Descricao,
                FornecedorID = novoMedicamento.Fornecedor.Id,
                novoMedicamento.QtdEstoque
            });
        }

        public void Editar(Guid idParaAtualizar, Medicamento medicamentoAtualizado)
        {
            const string sql = @"UPDATE TBMedicamento SET Nome = @Nome, Descricao = @Descricao, FornecedorID = @FornecedorID WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = idParaAtualizar,
                medicamentoAtualizado.Nome,
                medicamentoAtualizado.Descricao,
                FornecedorID = medicamentoAtualizado.Fornecedor.Id
            });
        }

        public void Excluir(Guid id)
        {
            const string sql = @"DELETE FROM TBMedicamento WHERE Id = @Id";

            connection.Execute(sql, new
            {
                Id = id
            });
        }

        public List<Medicamento> ObterRegistros()
        {
            const string sql = @"SELECT m.Id, m.Nome, m.Descricao, m.QtdEstoque, m.FornecedorID 
                                        FROM TBMedicamento m INNER JOIN TBFornecedor f ON m.FornecedorID = f.Id ORDER BY m.Nome ASC";

            var medicamentos = connection.Query<Medicamento, Fornecedor, Medicamento>(
                sql, 
                map: (med, forn) =>
                {
                    med.Fornecedor = forn;
                    return med;
                },
                splitOn: "FornecedorID"
            ).ToList();

            return medicamentos;
        }

        public Medicamento? ObterRegistroPorID(Guid id)
        {
            const string sql = @"SELECT * FROM TBMedicamento m INNER JOIN TBFornecedor f ON m.FornecedorID = f.Id WHERE m.Id = @Id";

            var medicamento = connection.Query<Medicamento, Fornecedor, Medicamento>(
                sql,
                map: (med, forn) =>
                {
                    med.Fornecedor = forn;
                    return med;
                },
                param: new { Id = id },
                splitOn: "FornecedorID"
            ).FirstOrDefault();

            return medicamento;
        }

        public bool RegistroDuplicado(Medicamento medicamento)
        {
            const string sql = @"SELECT * FROM TBMedicamento m INNER JOIN TBFornecedor f ON m.FornecedorID = f.Id";

            var medicamentos = connection.Query<Medicamento, Fornecedor, Medicamento>(
                sql,
                map: (med, forn) =>
                {
                    med.Fornecedor = forn;
                    return med;
                },
                param: new { medicamento.Id },
                splitOn: "FornecedorID"
            ).ToList();

            foreach (Medicamento m in medicamentos)
                if (m.Nome == medicamento.Nome && m.Id != medicamento.Id)
                    return true;

            return false;
        }
    }
}