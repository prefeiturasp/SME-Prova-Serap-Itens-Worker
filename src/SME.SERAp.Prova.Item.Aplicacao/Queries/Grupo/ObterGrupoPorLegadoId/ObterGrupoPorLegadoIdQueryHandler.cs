using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterGrupoPorLegadoIdQueryHandler : IRequestHandler<ObterGrupoPorLegadoIdQuery, Grupo>
    {
        private readonly IRepositorioGrupo repositorioGrupo;

        public ObterGrupoPorLegadoIdQueryHandler(IRepositorioGrupo repositorioGrupo)
        {
            this.repositorioGrupo = repositorioGrupo ?? throw new ArgumentNullException(nameof(repositorioGrupo));
        }

        public async Task<Grupo> Handle(ObterGrupoPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioGrupo.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
