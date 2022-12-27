using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaPorLegadoIdQueryHandler : IRequestHandler<ObterAreaPorLegadoIdQuery, AreaConhecimento>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterAreaPorLegadoIdQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<AreaConhecimento> Handle(ObterAreaPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
