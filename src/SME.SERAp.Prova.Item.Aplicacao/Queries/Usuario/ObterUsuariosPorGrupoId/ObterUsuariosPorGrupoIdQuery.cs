using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuariosPorGrupoIdQuery : IRequest<IEnumerable<Usuario>>
    {
        public ObterUsuariosPorGrupoIdQuery(long grupoId)
        {
            GrupoId = grupoId;
        }

        public long GrupoId { get; set; }
    }
}
