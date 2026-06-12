using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDificuldadePorIdQueryHandler : IRequestHandler<ObterDificuldadePorIdQuery, Dificuldade>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public ObterDificuldadePorIdQueryHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<Dificuldade> Handle(ObterDificuldadePorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.ObterPorIdAsync(request.Id);
        }
    }
}