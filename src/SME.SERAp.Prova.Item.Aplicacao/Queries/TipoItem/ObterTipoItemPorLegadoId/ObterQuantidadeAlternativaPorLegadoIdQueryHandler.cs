using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQuantidadeAlternativaPorLegadoIdQueryHandler : IRequestHandler<ObterQuantidadeAlternativa, QuantidadeAlternativa>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa;

        public ObterQuantidadeAlternativaPorLegadoIdQueryHandler(IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa)
        {
            this.repositorioQuantidadeAlternativa = repositorioQuantidadeAlternativa ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativa));
        }

        public async Task<QuantidadeAlternativa> Handle(ObterQuantidadeAlternativa request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativa.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
