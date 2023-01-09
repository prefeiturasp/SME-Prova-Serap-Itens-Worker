using SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories.CoreSSO
{
    public class RepositorioCoreSSOGrupo : RepositorioCoreSSO, IRepositorioCoreSSOGrupo
    {
        public RepositorioCoreSSOGrupo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<GrupoCoressoDto>> ObterGruposAtivosAsync(long sistemaId, long moduloId)
        {
            var conexao = ObterConexao();

            try
            {
                var query = @"select g.gru_id as id, g.gru_nome as nome, gp.grp_consultar as permiteConsultar, gp.grp_inserir as permiteInserir, gp.grp_alterar as permiteAlterar, gp.grp_excluir as permiteExcluir
                              from sys_grupo g
                              left join SYS_GrupoPermissao gp on gp.sis_id = g.sis_id and gp.gru_id = g.gru_id
                              where g.gru_situacao = 1 and g.sis_id = @sistemaId and gp.mod_id = @moduloId";

                return await conexao.QueryAsync<GrupoCoressoDto>(query, new { sistemaId, moduloId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }
    }
}
