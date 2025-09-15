using System.Data;

namespace ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento
{
    public class RepositorioMedicamentoEmSql
    {
        private readonly IDbConnection connection;

        public RepositorioMedicamentoEmSql(IDbConnection connection)
        {
            this.connection = connection;
        }
    }
}