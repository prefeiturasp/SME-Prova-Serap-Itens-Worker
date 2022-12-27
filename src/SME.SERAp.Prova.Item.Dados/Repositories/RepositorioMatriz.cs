
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.ApplicationInsights;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioMatriz : RepositorioBase<Matriz>, IRepositorioMatriz
    {
        public RepositorioMatriz(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        { }
        public async Task<Matriz> ObterPorLegadoIdAsync(long legadoId)
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
                                from matriz where legado_id = @legadoId"
                ;

                return await conn.QueryFirstOrDefaultAsync<Matriz>(query, new { legadoId });
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
        public async Task<IEnumerable<Matriz>> ObterPorDisciplinaId(long disciplinaId)
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
                                from matriz where disciplina_id = @disciplina_id";

                return await conn.QueryAsync<Matriz>(query, new { disciplinaId });
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
