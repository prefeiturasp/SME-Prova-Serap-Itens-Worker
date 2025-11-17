using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioItem : RepositorioBase<Dominio.Entities.Item>, IRepositorioItem
    {
        public RepositorioItem(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<Dominio.Entities.Item> ObterPorId(long itemId)
        {
            const string queryItem = @"
                                        SELECT * FROM item 
                                        WHERE id = @itemId;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<Dominio.Entities.Item>(queryItem, new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<Alternativa>> ObterAlternativasPorItemId(long itemId)
        {
            const string query = @"
                                    SELECT
                                        id,
                                        item_id ItemId,
                                        descricao,
                                        ordem,
                                        numeracao
                                    FROM alternativa
                                    WHERE item_id = @itemId
                                    ORDER BY ordem;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryAsync<Alternativa>(query, new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}