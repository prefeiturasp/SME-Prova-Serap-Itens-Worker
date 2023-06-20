using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioCompetencia : RepositorioBase<Competencia>, IRepositorioCompetencia
    {
        public RepositorioCompetencia(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<Competencia> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     matriz_id as MatrizId,
                                     codigo,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from competencia where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Competencia>(query, new { legadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<Competencia>> ObterPorMatrizLegadoIdAsync(long matrizLegadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select c.id, 
                                     c.legado_id as LegadoId,
                                     c.matriz_id as MatrizId,
                                     c.codigo,
                                     c.descricao,
                                     c.criado_em as CriadoEm,
                                     c.alterado_em as AlteradoEm,
                                     c.status
                                from competencia c
                                    inner join matriz m on c.matriz_id = m.id
                                where m.legado_id = @matrizLegadoId";

                return await conn.QueryAsync<Competencia>(query, new { matrizLegadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

    }
}
