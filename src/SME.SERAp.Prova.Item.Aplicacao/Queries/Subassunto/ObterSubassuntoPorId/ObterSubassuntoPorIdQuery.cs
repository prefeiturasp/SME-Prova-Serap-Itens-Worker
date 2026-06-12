using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntoPorIdQuery : IRequest<Subassunto>
    {
        public ObterSubassuntoPorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}