using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciaPorIdQuery : IRequest<Competencia>
    {
        public ObterCompetenciaPorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}