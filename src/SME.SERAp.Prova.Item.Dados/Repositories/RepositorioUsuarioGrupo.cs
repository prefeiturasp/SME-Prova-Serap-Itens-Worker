using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioUsuarioGrupo : RepositorioBase<UsuarioGrupo>, IRepositorioUsuarioGrupo
    {
        public RepositorioUsuarioGrupo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<UsuarioGrupo> ObterPorUsuarioIdGrupoIdAsync(long usuarioId, long grupoId)
        {
            var conexao = ObterConexaoLeitura();
            try
            {
                var query = "select id, usuario_id, grupo_id, criado_em, alterado_em, status from usuario_grupo where usuario_id = @usuarioId and grupo_id = @grupoId";
                return await conexao.QueryFirstOrDefaultAsync<UsuarioGrupo>(query, new { usuarioId, grupoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }
    }
}
