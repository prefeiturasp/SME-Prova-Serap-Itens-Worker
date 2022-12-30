using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorLegadoIdQueryHandler : IRequestHandler<ObterTipoGradePorLegadoIdQuery, TipoGrade>
    {
        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public ObterTipoGradePorLegadoIdQueryHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<TipoGrade> Handle(ObterTipoGradePorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
