using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioHabilidade : RepositorioBase<Habilidade>, IRepositorioHabilidade
    {
        public RepositorioHabilidade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<IEnumerable<Habilidade>> ObterPorCompetenciaLegadoIdAsync(long competenciaLegadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select h.id, 
                                     h.legado_id as LegadoId,
                                     h.competencia_id as CompetenciaId,
                                     h.codigo,
                                     h.descricao,
                                     h.criado_em as CriadoEm,
                                     h.alterado_em as AlteradoEm,
                                     h.status
                                from habilidade h 
                                inner join competencia c on c.id = h.competencia_id
                                where c.legado_id = @competenciaLegadoId";

                return await conn.QueryAsync<Habilidade>(query, new { competenciaLegadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<Habilidade> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     competencia_id as CompetenciaId,
                                     codigo,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from habilidade where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Habilidade>(query, new { legadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
