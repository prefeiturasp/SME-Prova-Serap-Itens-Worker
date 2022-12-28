using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioAssunto : RepositorioBase<Assunto>, IRepositorioAssunto
    {
        public RepositorioAssunto(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<Assunto> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId, 
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from assunto where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Assunto>(query, new { legadoId });
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
