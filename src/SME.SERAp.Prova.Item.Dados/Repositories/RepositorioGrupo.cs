using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioGrupo : RepositorioBase<Grupo>, IRepositorioGrupo
    {
        public RepositorioGrupo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public virtual async Task<Grupo> ObterPorLegadoIdAsync(Guid legadoId)
        {
            var conexao = ObterConexaoLeitura();
            try
            {
                var query = @"select id, legado_id, nome, permite_consultar, permite_inserir, permite_alterar, permite_excluir, criado_em, alterado_em, status 
                              from grupo 
                              where legado_id = @legadoId";

                return await conexao.QueryFirstOrDefaultAsync<Grupo>(query, new { legadoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }

    }
}
