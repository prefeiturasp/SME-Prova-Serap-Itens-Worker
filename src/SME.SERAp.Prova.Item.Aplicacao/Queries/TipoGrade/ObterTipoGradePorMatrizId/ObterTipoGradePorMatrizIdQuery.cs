using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorMatrizIdQuery : IRequest<IEnumerable<TipoGrade>>
    {
        public ObterTipoGradePorMatrizIdQuery(long matrizId)
        {
            MatrizId = matrizId;
        }

        public long MatrizId { get; }
    }
}