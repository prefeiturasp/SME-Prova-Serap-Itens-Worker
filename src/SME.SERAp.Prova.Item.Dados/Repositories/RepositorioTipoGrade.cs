using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioTipoGrade : RepositorioBase<TipoGrade>, IRepositorioTipoGrade
    {
        public RepositorioTipoGrade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<TipoGrade> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     matriz_id as MatrizId,
                                     descricao,
                                     ordem,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from tipo_grade where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<TipoGrade>(query, new { legadoId });
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

        public async Task<IEnumerable<TipoGrade>> ObterPorMatrizLegadoIdAsync(long matrizLegadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select tg.id, 
                                     tg.legado_id as LegadoId,
                                     tg.matriz_id as MatrizId,
                                     tg.descricao,
                                     tg.ordem,
                                     tg.criado_em as CriadoEm,
                                     tg.alterado_em as AlteradoEm,
                                     tg.status
                                from tipo_grade tg
                                    inner join matriz m on tg.matriz_id = m.id
                                where m.legado_id = @matrizLegadoId";

                return await conn.QueryAsync<TipoGrade>(query, new { matrizLegadoId });
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

        public async Task<IEnumerable<TipoGrade>> ObterPorMatrizIdAsync(long matrizId)
        {
            using var conn = ObterConexao();
            try
            {
                const string query = @"select tg.id, 
                                         tg.legado_id as LegadoId,
                                         tg.matriz_id as MatrizId,
                                         tg.descricao,
                                         tg.ordem,
                                         tg.criado_em as CriadoEm,
                                         tg.alterado_em as AlteradoEm,
                                         tg.status
                                        from tipo_grade tg
                                        where tg.matriz_id = @matrizId";

                return await conn.QueryAsync<TipoGrade>(query, new { matrizId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
