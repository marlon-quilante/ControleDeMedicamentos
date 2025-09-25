using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using Dapper;
using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPrescricao
{
    public class RepositorioPrescricaoEmSql (IDbConnection connection)
    {
        public void Cadastrar(Prescricao novaPrescricao)
        {
            const string sqlInsertPrescricao = @"INSERT INTO TBPrescricao (Id, Descricao, DataEmissao, DataValidade, CrmMedico, PacienteID) VALUES (@Id, @Descricao, @DataEmissao, @DataValidade, @CrmMedico, @PacienteID)";

            connection.Open();

            var transaction = connection.BeginTransaction();

            connection.Execute(sqlInsertPrescricao, new
            {
                novaPrescricao.Id,
                novaPrescricao.Descricao,
                novaPrescricao.DataEmissao,
                novaPrescricao.DataValidade,
                novaPrescricao.CrmMedico,
                PacienteID = novaPrescricao.Paciente.Id
            }, transaction);

            const string sqlInsertMedicamentoPrescrito = @"INSERT INTO TBMedicamentoPrescrito (Id, PrescricaoID, MedicamentoID, Dosagem, Periodo, Quantidade) VALUES (@Id, @PrescricaoID, @MedicamentoID, @Dosagem, @Periodo, @Quantidade)";

            foreach (var med in novaPrescricao.MedicamentosPrescritos)
            {
                connection.Execute(sqlInsertMedicamentoPrescrito, new
                {
                    med.Id,
                    PrescricaoID = novaPrescricao.Id,
                    MedicamentoID = med.Medicamento.Id,
                    med.Dosagem,
                    med.Periodo,
                    med.Quantidade
                }, transaction);
            }

            transaction.Commit();
        }

        public void Editar(Guid id, Prescricao prescricaoAtualizada)
        {
            const string sqlUpdatePrescricao = @"UPDATE TBPrescricao SET Descricao = @Descricao, DataValidade = @DataValidade, CrmMedico = @CrmMedico, PacienteID = @PacienteID WHERE Id = @Id";

            connection.Open();

            var transaction = connection.BeginTransaction();

            connection.Execute(sqlUpdatePrescricao, new
            {
                prescricaoAtualizada.Descricao,
                prescricaoAtualizada.DataValidade,
                prescricaoAtualizada.CrmMedico,
                PacienteID = prescricaoAtualizada.Paciente.Id,
                prescricaoAtualizada.Id
            }, transaction);

            const string sqlDeleteMedicamentos = @"DELETE FROM TBMedicamentoPrescrito WHERE PrescricaoID = @PrescricaoID";

            connection.Execute(sqlDeleteMedicamentos, new { PrescricaoID = id }, transaction);

            const string sqlInsertMedicamentoPrescrito = @"INSERT INTO TBMedicamentoPrescrito (Id, PrescricaoID, MedicamentoID, Dosagem, Periodo, Quantidade) VALUES (@Id, @PrescricaoID, @MedicamentoID, @Dosagem, @Periodo, @Quantidade)";

            foreach (var med in prescricaoAtualizada.MedicamentosPrescritos)
            {
                connection.Execute(sqlInsertMedicamentoPrescrito, new
                {
                    med.Id,
                    PrescricaoID = id,
                    MedicamentoID = med.Medicamento.Id,
                    med.Dosagem,
                    med.Periodo,
                    med.Quantidade
                }, transaction);
            }

            transaction.Commit();
        }

        public void Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Prescricao> ObterRegistros()
        {
            const string sqlSelectPrescricoes = @"SELECT * FROM TBPrescricao p INNER JOIN TBPaciente pa ON p.PacienteID = pa.Id";

            var prescricoes = connection.Query<Prescricao, Paciente, Prescricao>(sqlSelectPrescricoes, (p, pa) => { p.Paciente = pa; return p; }, splitOn: "PacienteID").ToList();

            const string sqlSelectMedicamentos = @"SELECT 
                                                        mp.Id, mp.Dosagem, mp.Periodo, mp.Quantidade, mp.PrescricaoID, mp.MedicamentoID,
                                                        m.Id AS MedicamentoID, m.Id, m.Nome, m.Descricao, m.FornecedorID as FornecedorID,
                                                        f.Id AS FornecedorID, f.Id, f.Nome, f.Telefone, f.CNPJ,
                                                        p.Id AS PrescricaoID, p.Id
                                                        FROM TBMedicamentoPrescrito mp INNER JOIN TBMedicamento m ON m.Id = mp.MedicamentoID INNER JOIN TBFornecedor f ON f.Id = m.FornecedorID INNER JOIN TBPrescricao p ON p.Id = mp.PrescricaoID";

            var medicamentosPrescritos = connection.Query<MedicamentoPrescrito, Medicamento, Fornecedor, Prescricao, (Guid PrescricaoID, MedicamentoPrescrito MP)>(sqlSelectMedicamentos, (mp, m, f, p) =>
            {
                m.Fornecedor = f;
                mp.Medicamento = m;
                mp.Prescricao = p;

                return (p.Id, mp);
            }, splitOn: "MedicamentoID, FornecedorID, PrescricaoID");

            var dicionario = medicamentosPrescritos.ToLookup(x => x.PrescricaoID, x => x.MP);

            foreach (var prescricao in prescricoes)
                prescricao.MedicamentosPrescritos = dicionario[prescricao.Id].ToList();

            return prescricoes;
        }

        public Prescricao? ObterRegistroPorID(Guid id)
        {
            return ObterRegistros().FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
