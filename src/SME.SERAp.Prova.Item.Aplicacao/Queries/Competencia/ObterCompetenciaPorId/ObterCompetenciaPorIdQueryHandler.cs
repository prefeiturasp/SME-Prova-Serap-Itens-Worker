using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciaPorIdQueryHandler : IRequestHandler<ObterCompetenciaPorIdQuery, Competencia>
    {
        private readonly IRepositorioCompetencia repositorioCompetencia;

        public ObterCompetenciaPorIdQueryHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<Competencia> Handle(ObterCompetenciaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.ObterPorIdAsync(request.Id);
        }
    }
}