using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDificuldadePorIdQuery : IRequest<Dificuldade>
    {
        public ObterDificuldadePorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}