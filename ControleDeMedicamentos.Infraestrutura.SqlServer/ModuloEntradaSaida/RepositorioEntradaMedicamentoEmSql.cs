using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloEntradaSaida
{
    public class RepositorioEntradaMedicamentoEmSql (IDbConnection connection)
    {
        public void Cadastrar(EntradaMedicamento novaEntrada)
        {
            const string sqlInsertEntrada = @"INSERT INTO TBEntradaMedicamento (Id, FuncionarioID, MedicamentoID, QtdEntrada, DataEntrada) 
                                                     VALUES (@Id, @FuncionarioID, @MedicamentoID, @QtdEntrada, @DataEntrada)";

            if (novaEntrada != null)
            {
                connection.Execute(sqlInsertEntrada, new
                {
                    novaEntrada.Id,
                    FuncionarioID = novaEntrada.Funcionario.Id,
                    MedicamentoID = novaEntrada.Medicamento.Id,
                    novaEntrada.QtdEntrada,
                    novaEntrada.DataEntrada
                });
            }

            AumentarEstoqueMedicamento(novaEntrada);
        }

        public List<EntradaMedicamento> ObterRegistros()
        {
            const string sqlSelectEntradas = @"SELECT 
                                                        em.Id AS EntradaMedicamentoID, em.Id, em.FuncionarioID, em.MedicamentoID, em.QtdEntrada, em.DataEntrada,
                                                        m.Id AS MedicamentoID, m.Id, m.Nome, m.Descricao, m.QtdEstoque,
                                                        f.Id AS FuncionarioID, f.Id, f.Nome, f.Telefone, f.CPF
                                                        
                                                        FROM TBEntradaMedicamento em INNER JOIN 
                                                             TBMedicamento m ON em.MedicamentoID = m.Id INNER JOIN
                                                             TBFuncionario f ON em.FuncionarioID = f.Id";

            var entradasMedicamento = connection.Query<EntradaMedicamento, Medicamento, Funcionario, EntradaMedicamento>(sqlSelectEntradas, (em, m, f) =>
            {
                em.Funcionario = f;
                em.Medicamento = m;

                return em;
            }, splitOn: "EntradaMedicamentoID, MedicamentoID, FuncionarioID").ToList();

            return entradasMedicamento;
        }

        public void AumentarEstoqueMedicamento(EntradaMedicamento novaEntrada)
        {
            const string sqlUpdateEstoqueMedicamento = @"UPDATE TBMedicamento SET QtdEstoque = QtdEstoque + @QtdEntrada WHERE Id = @Id";

            connection.Execute(sqlUpdateEstoqueMedicamento, new
            {
                novaEntrada.QtdEntrada,
                novaEntrada.Medicamento.Id
            });
        }
    }
}
