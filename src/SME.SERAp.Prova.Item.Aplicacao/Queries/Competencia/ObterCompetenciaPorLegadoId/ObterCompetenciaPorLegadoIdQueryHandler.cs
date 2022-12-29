using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciaPorLegadoIdQueryHandler : IRequestHandler<ObterCompetenciaPorLegadoIdQuery, Competencia>
    {
        private readonly IRepositorioCompetencia repositorioCompetencia;

        public ObterCompetenciaPorLegadoIdQueryHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<Competencia> Handle(ObterCompetenciaPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
