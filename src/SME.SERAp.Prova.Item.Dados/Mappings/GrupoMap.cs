using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class GrupoMap : DommelEntityMap<Grupo>
    {
        public GrupoMap()
        {
            ToTable("grupo");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.PermiteConsultar).ToColumn("permite_consultar");
            Map(c => c.PermiteInserir).ToColumn("permite_inserir");
            Map(c => c.PermiteAlterar).ToColumn("permite_alterar");
            Map(c => c.PermiteExcluir).ToColumn("permite_excluir");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
