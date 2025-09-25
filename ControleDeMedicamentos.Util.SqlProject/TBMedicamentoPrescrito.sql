CREATE TABLE [dbo].[TBMedicamentoPrescrito]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PrescricaoID] UNIQUEIDENTIFIER NULL, 
    [MedicamentoID] UNIQUEIDENTIFIER NULL, 
    [Dosagem] NVARCHAR(100) NULL, 
    [Periodo] NVARCHAR(100) NULL, 
    [Quantidade] INT NULL, 
    CONSTRAINT [FK_TBMedicamentoPrescrito_TBPrescricao] FOREIGN KEY (PrescricaoID) REFERENCES TBPrescricao(Id), 
    CONSTRAINT [FK_TBMedicamentoPrescrito_TBMedicamento] FOREIGN KEY (MedicamentoID) REFERENCES TBMedicamento(Id)
)
