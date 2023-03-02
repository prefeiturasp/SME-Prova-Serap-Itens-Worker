using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterGruposCoreSSOQueryHandler : IRequestHandler<ObterGruposCoreSSOQuery, IEnumerable<GrupoCoressoDto>>
    {
        private readonly IRepositorioCoreSSOGrupo repositorioCoreSSOGrupo;

        public ObterGruposCoreSSOQueryHandler(IRepositorioCoreSSOGrupo repositorioCoreSSOGrupo)
        {
            this.repositorioCoreSSOGrupo = repositorioCoreSSOGrupo ?? throw new ArgumentNullException(nameof(repositorioCoreSSOGrupo));
        }

        public async Task<IEnumerable<GrupoCoressoDto>> Handle(ObterGruposCoreSSOQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCoreSSOGrupo.ObterGruposAtivosAsync(request.SistemaId, request.ModuloId);
        }
    }
}
