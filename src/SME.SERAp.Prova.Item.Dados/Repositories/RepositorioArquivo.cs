using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioArquivo : RepositorioBase<Assunto>, IRepositorioArquivo
    {
        public RepositorioArquivo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<ItemArquivoDto>> ObterAudiosPorItemIdAsync(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"
                                SELECT
                                    ia.id,
                                    ia.arquivo_id as ArquivoId,
                                    a.legado_id AS LegadoId,
                                    a.nome AS NomeArquivo,
                                    a.content_type AS ContentType
                                FROM
                                    item_audio ia
                                INNER JOIN
                                    arquivo a ON ia.arquivo_id = a.id
                                WHERE
                                    ia.item_id = @ItemId AND ia.situacao = 1;
                ";

                return await conn.QueryAsync<ItemArquivoDto>(query, new { ItemId = itemId });
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

        public async Task<IEnumerable<ItemArquivoDto>> ObterVideosPorItemIdAsync(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"
                                SELECT
                                    iv.id,
                                    iv.arquivo_id as ArquivoId,
                                    a.legado_id AS LegadoId,
                                    a.nome AS NomeArquivo,
                                    a.content_type AS ContentType
                                FROM
                                    item_video iv
                                INNER JOIN
                                    arquivo a ON iv.arquivo_id = a.id
                                WHERE
                                    iv.item_id = @ItemId AND iv.situacao = 1;
                ";

                return await conn.QueryAsync<ItemArquivoDto>(query, new { ItemId = itemId });
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
    }
}