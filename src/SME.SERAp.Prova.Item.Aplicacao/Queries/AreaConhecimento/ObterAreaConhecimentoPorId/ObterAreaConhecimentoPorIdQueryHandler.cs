using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaConhecimentoPorIdQueryHandler : IRequestHandler<ObterAreaConhecimentoPorIdQuery, AreaConhecimento>
    {
        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterAreaConhecimentoPorIdQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<AreaConhecimento> Handle(ObterAreaConhecimentoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterPorIdAsync(request.Id);
        }
    }
}