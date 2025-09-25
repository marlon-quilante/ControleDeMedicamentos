CREATE TABLE [dbo].[TBPrescricao]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Descricao] NVARCHAR(500) NULL, 
    [DataEmissao] DATETIME NULL, 
    [DataValidade] DATETIME NULL, 
    [CrmMedico] NVARCHAR(50) NULL,
    [PacienteID] UNIQUEIDENTIFIER NULL,  
    CONSTRAINT [FK_TBPrescricao_TBPaciente] FOREIGN KEY (PacienteID) REFERENCES TBPaciente(Id)
)
