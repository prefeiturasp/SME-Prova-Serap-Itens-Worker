using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorIdQuery : IRequest<Matriz>
    {
        public ObterMatrizPorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}