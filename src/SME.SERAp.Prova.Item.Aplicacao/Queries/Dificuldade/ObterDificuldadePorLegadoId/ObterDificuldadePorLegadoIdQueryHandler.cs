using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterDificuldadePorLegadoIdQueryHandler : IRequestHandler<ObterDificuldadePorLegadoIdQuery, Dificuldade>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public ObterDificuldadePorLegadoIdQueryHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<Dificuldade> Handle(ObterDificuldadePorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
