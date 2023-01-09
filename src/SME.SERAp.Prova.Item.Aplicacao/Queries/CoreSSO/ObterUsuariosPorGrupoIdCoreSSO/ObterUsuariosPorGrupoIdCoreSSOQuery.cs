using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuariosPorGrupoIdCoreSSOQuery : IRequest<IEnumerable<UsuarioCoressoDto>>
    {
        public ObterUsuariosPorGrupoIdCoreSSOQuery(long sistemaId, long moduloId, Guid grupoLegadoId)
        {
            SistemaId = sistemaId;
            ModuloId = moduloId;
            GrupoLegadoId = grupoLegadoId;
        }

        public long SistemaId { get; set; }
        public long ModuloId { get; set; }
        public Guid GrupoLegadoId { get; set; }
    }
}
