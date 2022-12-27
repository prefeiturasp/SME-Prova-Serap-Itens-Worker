using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizLegadoIdQuery : IRequest<IEnumerable<Competencia>>
    {
        public ObterCompetenciasPorMatrizLegadoIdQuery(long matrizLegadoId)
        {
            MatrizLegadoId = matrizLegadoId;
        }

        public long MatrizLegadoId { get; set; }

    }
}
