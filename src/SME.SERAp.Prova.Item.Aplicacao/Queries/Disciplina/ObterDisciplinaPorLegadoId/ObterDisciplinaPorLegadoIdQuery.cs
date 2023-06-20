using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinaPorLegadoIdQuery : IRequest<Disciplina>
    {
        public ObterDisciplinaPorLegadoIdQuery(long legadoId)
        {
            LegadoId = legadoId;
        }

        public long LegadoId { get; set; }
    }
}