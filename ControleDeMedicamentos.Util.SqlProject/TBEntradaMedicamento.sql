CREATE TABLE [dbo].[TBEntradaMedicamento]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FuncionarioID] UNIQUEIDENTIFIER NULL, 
    [MedicamentoID] UNIQUEIDENTIFIER NULL, 
    [QtdEntrada] INT NULL, 
    [DataEntrada] DATETIME NULL, 
    CONSTRAINT [FK_TBEntradaMedicamento_TBFuncionario] FOREIGN KEY (FuncionarioID) REFERENCES TBFuncionario(Id), 
    CONSTRAINT [FK_TBEntradaMedicamento_TBMedicamento] FOREIGN KEY (MedicamentoID) REFERENCES TBMedicamento(Id)
)
