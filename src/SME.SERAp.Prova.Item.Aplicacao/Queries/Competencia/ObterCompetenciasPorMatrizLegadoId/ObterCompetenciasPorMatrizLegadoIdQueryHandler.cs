using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizLegadoIdQueryHandler : IRequestHandler<ObterCompetenciasPorMatrizLegadoIdQuery, IEnumerable<Competencia>>
    {

        private readonly IRepositorioCompetencia repositorioCompetencia;

        public ObterCompetenciasPorMatrizLegadoIdQueryHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<IEnumerable<Competencia>> Handle(ObterCompetenciasPorMatrizLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.ObterPorMatrizLegadoIdAsync(request.MatrizLegadoId);
        }
    }
}
