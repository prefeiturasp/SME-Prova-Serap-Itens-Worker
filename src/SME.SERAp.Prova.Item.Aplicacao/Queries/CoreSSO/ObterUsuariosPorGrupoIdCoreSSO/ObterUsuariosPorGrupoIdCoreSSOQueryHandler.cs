using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.CoreSSO.ObterUsuariosCoreSSO
{
    public class ObterUsuariosPorGrupoIdCoreSSOQueryHandler : IRequestHandler<ObterUsuariosPorGrupoIdCoreSSOQuery, IEnumerable<UsuarioCoressoDto>>
    {
        private readonly IRepositorioCoreSSOUsuario repositorioCoreSSOUsuario;

        public ObterUsuariosPorGrupoIdCoreSSOQueryHandler(IRepositorioCoreSSOUsuario repositorioCoreSSOUsuario)
        {
            this.repositorioCoreSSOUsuario = repositorioCoreSSOUsuario ?? throw new ArgumentNullException(nameof(repositorioCoreSSOUsuario));
        }

        public async Task<IEnumerable<UsuarioCoressoDto>> Handle(ObterUsuariosPorGrupoIdCoreSSOQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCoreSSOUsuario.ObterUsuariosAtivosPorGrupoIdAsync(request.SistemaId, request.ModuloId, request.GrupoLegadoId);
        }
    }
}
