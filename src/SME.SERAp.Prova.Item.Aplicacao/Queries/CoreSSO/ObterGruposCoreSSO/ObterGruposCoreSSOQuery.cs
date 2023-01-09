using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterGruposCoreSSOQuery : IRequest<IEnumerable<GrupoCoressoDto>>
    {
        public ObterGruposCoreSSOQuery(long sistemaId, long moduloId)
        {
            SistemaId = sistemaId;
            ModuloId = moduloId;
        }

        public long SistemaId { get; set; }
        public long ModuloId { get; set; }
    }
}
