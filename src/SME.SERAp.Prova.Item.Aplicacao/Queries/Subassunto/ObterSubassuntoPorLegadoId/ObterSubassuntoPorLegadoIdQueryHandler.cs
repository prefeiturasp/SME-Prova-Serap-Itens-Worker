using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntoPorLegadoIdQueryHandler : IRequestHandler<ObterSubassuntoPorLegadoIdQuery, Subassunto>
    {

        private readonly IRepositorioSubassunto repositorioSubassunto;

        public ObterSubassuntoPorLegadoIdQueryHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioSubassunto = repositorioSubassunto ?? throw new ArgumentNullException(nameof(repositorioSubassunto));
        }

        public async Task<Subassunto> Handle(ObterSubassuntoPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSubassunto.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
