using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterGrupoPorLegadoIdQuery : IRequest<Grupo>
    {
        public ObterGrupoPorLegadoIdQuery(Guid legadoId)
        {
            LegadoId = legadoId;
        }

        public Guid LegadoId { get; set; }
    }
}
