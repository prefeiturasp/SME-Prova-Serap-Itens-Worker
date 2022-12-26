using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioTipoItem : RepositorioBase<TipoItem>, IRepositorioTipoItem
    {
        public RepositorioTipoItem(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<TipoItem> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     eh_padrao as EhPadrao,
                                     qtde_alternativa as QtdeAlternativa,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from tipo_item where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<TipoItem>(query, new { legadoId });
            }
            catch (Exception)
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
