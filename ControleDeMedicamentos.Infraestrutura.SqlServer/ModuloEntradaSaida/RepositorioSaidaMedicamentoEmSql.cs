using ControleDeMedicamentos.Dominio.ModuloEntradaSaida;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloEntradaSaida
{
    public class RepositorioSaidaMedicamentoEmSql(IDbConnection connection)
    {
        public void Cadastrar(SaidaMedicamento novaSaida)
        {
            if (novaSaida != null)
            {
                const string sqlInsertSaida = @"INSERT INTO TBSaidaMedicamento (Id, FuncionarioID, PrescricaoID, DataSaida)
                                                            VALUES (@Id, @FuncionarioID, @PrescricaoID, @DataSaida)";

                connection.Open();

                var transaction = connection.BeginTransaction();

                connection.Execute(sqlInsertSaida, new
                {
                    novaSaida.Id,
                    FuncionarioID = novaSaida.Funcionario.Id,
                    PrescricaoID = novaSaida.Prescricao.Id,
                    novaSaida.DataSaida
                }, transaction);

                DiminuirEstoqueMedicamento(novaSaida, transaction);

                transaction.Commit();
            }
        }

        public List<SaidaMedicamento> ObterRegistros()
        {
            const string sqlSelectSaidas = @"SELECT sm.Id AS SaidaMedicamentoID, sm.Id, sm.FuncionarioID, sm.PrescricaoID, sm.DataSaida,
                                                    f.Id as FuncionarioID, f.Id, f.Nome, f.Telefone, f.CPF,
                                                    p.Id as PrescricaoID, p.Id, p.PacienteID, p.Descricao, p.DataEmissao, p.DataValidade, p.CrmMedico,
                                                    pac.Id, pac.Nome, pac.Telefone, pac.NumCartaoSus, pac.CPF

                                                    FROM TBSaidaMedicamento sm
                                                    INNER JOIN TBFuncionario f ON f.Id = sm.FuncionarioID
                                                    INNER JOIN TBPrescricao p ON p.Id = sm.PrescricaoID
                                                    INNER JOIN TBPaciente pac ON pac.Id = p.PacienteID";

            var saidas = connection.Query<SaidaMedicamento, Funcionario, Prescricao, Paciente, SaidaMedicamento>
                (sqlSelectSaidas, (sm, f, p, pac) =>
                {
                    sm.Funcionario = f;
                    sm.Prescricao = p;
                    p.Paciente = pac;

                    return sm;
                }, splitOn: "SaidaMedicamentoID, FuncionarioID, PrescricaoID, PacienteID").ToList();

            const string sqlSelectMedicamentos = @"SELECT 
                                                    mp.Id as MedicamentoPrescritoID, mp.Id, mp.PrescricaoID, mp.MedicamentoID, mp.Dosagem, mp.Periodo, mp.Quantidade,
                                                    p.Id as PrescricaoID, p.Id, p.PacienteID, p.Descricao, p.DataEmissao, p.DataValidade, p.CrmMedico,
                                                    sm.Id AS SaidaMedicamentoID, sm.Id, sm.FuncionarioID, sm.PrescricaoID, sm.DataSaida,
                                                    f.Id as FuncionarioID, f.Id, f.Nome, f.Telefone, f.CPF,
                                                    m.Id as MedicamentoID, m.Id, m.Nome, m.Descricao, m.FornecedorID, m.QtdEstoque,
                                                    pac.Id as PacienteID, pac.Id, pac.Nome, pac.Telefone, pac.NumCartaoSus, pac.CPF

                                                    FROM TBMedicamentoPrescrito mp
                                                    INNER JOIN TBPrescricao p ON p.Id = mp.PrescricaoID
                                                    INNER JOIN TBSaidaMedicamento sm ON p.Id = sm.PrescricaoID
                                                    INNER JOIN TBFuncionario f ON f.Id = sm.FuncionarioID
                                                    INNER JOIN TBMedicamento m ON m.Id = mp.MedicamentoID
                                                    INNER JOIN TBPaciente pac ON pac.Id = p.PacienteID";

            var medicamentos = connection.Query<MedicamentoPrescrito, Prescricao, SaidaMedicamento, Funcionario, Medicamento, Paciente, (Guid SaidaMedicamentoID, MedicamentoPrescrito MP)>
                (sqlSelectMedicamentos, (mp, p, sm, f, m, pac) =>
                {
                    mp.Prescricao = p;
                    mp.Medicamento = m;
                    sm.Funcionario = f;
                    sm.Prescricao = p;

                    return (sm.Id, mp);
                }, splitOn: "PrescricaoID, SaidaMedicamentoID, FuncionarioID, MedicamentoID, PacienteID").ToList();

            var dicionario = medicamentos.ToLookup(x => x.SaidaMedicamentoID, x => x.MP);

            foreach (SaidaMedicamento saida in saidas)
            {
                saida.Prescricao.MedicamentosPrescritos = dicionario[saida.Id].ToList();
            }

            return saidas;
        }

        public void DiminuirEstoqueMedicamento(SaidaMedicamento novaSaida, IDbTransaction transaction)
        {
            const string sqlUpdateEstoqueMedicamento = @"UPDATE TBMedicamento SET QtdEstoque = QtdEstoque - @QtdSaida WHERE Id = @Id";

            foreach (MedicamentoPrescrito mp in novaSaida.Prescricao.MedicamentosPrescritos)
            {
                connection.Execute(sqlUpdateEstoqueMedicamento, new
                {
                    QtdSaida = mp.Quantidade,
                    mp.Medicamento.Id
                }, transaction);
            }
        }
    }
}
