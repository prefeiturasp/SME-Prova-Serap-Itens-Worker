using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioQuantidadeAlternativa : RepositorioBase<QuantidadeAlternativa>, IRepositorioQuantidadeAlternativa
    {
        public RepositorioQuantidadeAlternativa(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<QuantidadeAlternativa> ObterPorLegadoIdAsync(long legadoId)
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
                                from quantidade_alternativa
                                where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<QuantidadeAlternativa>(query, new { legadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
