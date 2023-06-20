using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioDisciplina : RepositorioBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplina(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<Disciplina> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId, 
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status,
                                     area_conhecimento_id as AreaConhecimentoId
                                from disciplina
                                where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Disciplina>(query, new { legadoId });
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

        public async Task<IEnumerable<Disciplina>> ObterPorAreaConhecimentoLegadoIdAsync(long areaConhecimentoLegadoId)
        {
            using var conn = ObterConexao();
            try
            {
                const string query = @"select d.id, 
                                         d.legado_id as LegadoId, 
                                         d.descricao,
                                         d.criado_em as CriadoEm,
                                         d.alterado_em as AlteradoEm,
                                         d.status,
                                         d.area_conhecimento_id as AreaConhecimentoId
                                        from disciplina d
                                        inner join area_conhecimento ac on ac.id = d.area_conhecimento_id
                                        where ac.legado_id = @areaConhecimentoLegadoId";

                return await conn.QueryAsync<Disciplina>(query, new { areaConhecimentoLegadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}