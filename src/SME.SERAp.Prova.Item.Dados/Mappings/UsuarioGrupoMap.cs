using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class UsuarioGrupoMap : DommelEntityMap<UsuarioGrupo>
    {
        public UsuarioGrupoMap()
        {
            ToTable("usuario_grupo");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.UsuarioId).ToColumn("usuario_id");
            Map(c => c.GrupoId).ToColumn("grupo_id");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
