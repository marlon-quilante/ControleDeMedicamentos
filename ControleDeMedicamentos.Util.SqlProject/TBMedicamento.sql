CREATE TABLE [dbo].[TBMedicamento]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(100) NULL, 
    [Descricao] NVARCHAR(500) NULL, 
    [FornecedorID] UNIQUEIDENTIFIER NULL, 
    [QtdEstoque] INT NULL, 
    CONSTRAINT [FK_TBMedicamento_TBFornecedor] FOREIGN KEY (FornecedorID) REFERENCES TBFornecedor(Id)
)
