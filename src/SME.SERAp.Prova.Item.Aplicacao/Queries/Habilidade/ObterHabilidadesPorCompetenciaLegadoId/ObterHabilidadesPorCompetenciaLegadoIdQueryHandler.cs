using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaLegadoIdQueryHandler : IRequestHandler<ObterHabilidadesPorCompetenciaLegadoIdQuery, IEnumerable<Habilidade>>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public ObterHabilidadesPorCompetenciaLegadoIdQueryHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<IEnumerable<Habilidade>> Handle(ObterHabilidadesPorCompetenciaLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.ObterPorCompetenciaLegadoIdAsync(request.CompetenciaLegadoId);
        }
    }
}
