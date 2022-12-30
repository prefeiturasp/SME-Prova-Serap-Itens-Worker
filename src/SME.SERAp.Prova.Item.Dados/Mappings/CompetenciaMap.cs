﻿using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Dados
{
    public class CompetenciaMap : DommelEntityMap<Competencia>
    {
        public CompetenciaMap()
        {
            ToTable("competencia");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.Codigo).ToColumn("codigo");
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.MatrizId).ToColumn("matriz_id");
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
