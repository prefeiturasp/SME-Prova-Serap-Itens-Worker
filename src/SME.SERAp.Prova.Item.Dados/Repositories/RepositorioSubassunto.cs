using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioSubassunto : RepositorioBase<Subassunto>, IRepositorioSubassunto
    {
        public RepositorioSubassunto(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<Subassunto> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     assunto_id as AssuntoId,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from subassunto where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Subassunto>(query, new { legadoId });
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

        public async Task<IEnumerable<Subassunto>> ObterPorAssuntoIdAsync(long assuntoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     assunto_id as AssuntoId,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from subassunto where assunto_id = @assuntoId";

                return await conn.QueryAsync<Subassunto>(query, new { assuntoId });
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
