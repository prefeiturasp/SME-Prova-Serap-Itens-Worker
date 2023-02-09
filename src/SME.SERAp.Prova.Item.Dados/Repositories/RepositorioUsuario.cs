using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<Usuario>> ObterPorGrupoIdAsync(long grupoId)
        {
            var conexao = ObterConexaoLeitura();
            try
            {
                var query = @"select u.id, u.legado_id, u.login, u.nome, u.criado_em, u.alterado_em, u.status 
                              from usuario_grupo ug
                              left join usuario u on u.id = ug.usuario_id
                              where ug.grupo_id = @grupoId";

                return await conexao.QueryAsync<Usuario>(query, new { grupoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }

        public async Task<Usuario> ObterPorLegadoIdAsync(Guid legadoId)
        {
            var conexao = ObterConexaoLeitura();
            try
            {
                var query = @"select id, legado_id, login, nome, criado_em, alterado_em, status 
                              from usuario
                              where legado_id = @legadoId";

                return await conexao.QueryFirstOrDefaultAsync<Usuario>(query, new { legadoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }
    }
}
