using SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories.CoreSSO
{
    public class RepositorioCoreSSOUsuario : RepositorioCoreSSO, IRepositorioCoreSSOUsuario
    {
        public RepositorioCoreSSOUsuario(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<UsuarioCoressoDto>> ObterUsuariosAtivosPorGrupoIdAsync(long sistemaId, long moduloId, Guid grupoLegadoId)
        {
            var conexao = ObterConexao();

            try
            {
                var query = @"select distinct u.usu_id as [id], u.usu_login as [login], p.pes_nome as [nome]
                              from sys_grupo g
                              inner join SYS_GrupoPermissao gp on gp.sis_id = g.sis_id and gp.gru_id = g.gru_id
                              inner join SYS_UsuarioGrupo ug on ug.gru_id = g.gru_id
                              inner join SYS_Usuario u on u.usu_id = ug.usu_id
                              inner join PES_Pessoa p on p.pes_id = u.pes_id
                              where g.gru_situacao = 1 
                                and ug.usg_situacao = 1 
                                and u.usu_situacao = 1
                                and g.sis_id = @sistemaId
                                and gp.mod_id = @moduloId
                                and g.gru_id = @grupoLegadoId ";

                return await conexao.QueryAsync<UsuarioCoressoDto>(query, new { sistemaId, moduloId, grupoLegadoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }
    }
}
