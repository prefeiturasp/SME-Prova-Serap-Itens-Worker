using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntoPorIdQueryHandler : IRequestHandler<ObterSubassuntoPorIdQuery, Subassunto>
    {
        private readonly IRepositorioSubassunto repositorioSubassunto;

        public ObterSubassuntoPorIdQueryHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioSubassunto = repositorioSubassunto ?? throw new ArgumentNullException(nameof(repositorioSubassunto));
        }

        public async Task<Subassunto> Handle(ObterSubassuntoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSubassunto.ObterPorIdAsync(request.Id);
        }
    }
}