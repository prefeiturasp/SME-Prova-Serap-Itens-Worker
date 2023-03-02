using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuariosPorGrupoIdQueryHandler : IRequestHandler<ObterUsuariosPorGrupoIdQuery, IEnumerable<Usuario>>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public ObterUsuariosPorGrupoIdQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<IEnumerable<Usuario>> Handle(ObterUsuariosPorGrupoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioUsuario.ObterPorGrupoIdAsync(request.GrupoId);
        }
    }
}
