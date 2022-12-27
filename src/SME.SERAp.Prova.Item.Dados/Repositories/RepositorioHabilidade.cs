using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioHabilidade : RepositorioBase<Habilidade>, IRepositorioHabilidade
    {
        public RepositorioHabilidade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<Habilidade> ObterPorLegadoIdAsync(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId,
                                     componente_id as ComponenteId,
                                     codigo,
                                     descricao,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from habilidade where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Habilidade>(query, new { legadoId });
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
