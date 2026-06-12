using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadePorIdQueryHandler : IRequestHandler<ObterHabilidadePorIdQuery, Habilidade>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public ObterHabilidadePorIdQueryHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<Habilidade> Handle(ObterHabilidadePorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.ObterPorIdAsync(request.Id);
        }
    }
}