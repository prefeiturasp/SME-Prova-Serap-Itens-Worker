using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuarioGrupoPorUsuarioIdGrupoIdQueryHandler : IRequestHandler<ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery, UsuarioGrupo>
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;

        public ObterUsuarioGrupoPorUsuarioIdGrupoIdQueryHandler(IRepositorioUsuarioGrupo repositorioUsuarioGrupo)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
        }

        public async Task<UsuarioGrupo> Handle(ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioUsuarioGrupo.ObterPorUsuarioIdGrupoIdAsync(request.UsuarioId, request.GrupoId);
        }
    }
}
