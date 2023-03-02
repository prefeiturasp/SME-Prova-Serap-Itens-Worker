using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorMatrizIdQueryHandler : IRequestHandler<ObterTipoGradePorMatrizIdQuery, IEnumerable<TipoGrade>>
    {
        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public ObterTipoGradePorMatrizIdQueryHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<IEnumerable<TipoGrade>> Handle(ObterTipoGradePorMatrizIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.ObterPorMatrizIdAsync(request.MatrizId);
        }
    }
}