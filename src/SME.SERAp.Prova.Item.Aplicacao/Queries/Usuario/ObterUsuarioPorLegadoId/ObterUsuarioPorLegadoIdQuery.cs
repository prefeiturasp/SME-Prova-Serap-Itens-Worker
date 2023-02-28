using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuarioPorLegadoIdQuery : IRequest<Usuario>
    {
        public ObterUsuarioPorLegadoIdQuery(Guid legadoId)
        {
            LegadoId = legadoId;
        }

        public Guid LegadoId { get; set; }
    }
}
