using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioAreaConhecimento : RepositorioBase<AreaConhecimento>, IRepositorioAreaConhecimento
    {
        public RepositorioAreaConhecimento(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<AreaConhecimento> ObterPorLegadoIdAsync(long legadoId)
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
                                from area_conhecimento
                                where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<AreaConhecimento>(query, new { legadoId });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}