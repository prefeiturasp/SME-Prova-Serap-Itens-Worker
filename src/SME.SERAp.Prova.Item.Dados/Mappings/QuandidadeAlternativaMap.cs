using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Dados
{
    public class QuandidadeAlternativaMap : DommelEntityMap<QuantidadeAlternativa>
    {
        public QuandidadeAlternativaMap()
        {
            ToTable("quantidade_alternativa");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.EhPadrao).ToColumn("eh_padrao");
            Map(c => c.QtdeAlternativa).ToColumn("qtde_alternativa");
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
