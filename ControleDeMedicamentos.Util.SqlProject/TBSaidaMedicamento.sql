CREATE TABLE [dbo].[TBSaidaMedicamento]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FuncionarioID] UNIQUEIDENTIFIER NULL, 
    [PrescricaoID] UNIQUEIDENTIFIER NULL, 
    [DataSaida] DATETIME NULL, 
    CONSTRAINT [FK_TBSaidaMedicamento_TBFuncionario] FOREIGN KEY (FuncionarioID) REFERENCES TBFuncionario(Id), 
    CONSTRAINT [FK_TBSaidaMedicamento_TBPrescricao] FOREIGN KEY (PrescricaoID) REFERENCES TBPrescricao(Id)
)
