using Microsoft.Data.SqlClient;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Data;

namespace SME.SERAp.Prova.Item.Dados.Repositories.CoreSSO
{
    public abstract class RepositorioCoreSSO
    {
        private readonly ConnectionStringOptions connectionStrings;

        public RepositorioCoreSSO(ConnectionStringOptions connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        protected IDbConnection ObterConexao()
        {
            var conexao = new SqlConnection(connectionStrings.CoreSSO);
            conexao.Open();
            return conexao;
        }
    }
}
