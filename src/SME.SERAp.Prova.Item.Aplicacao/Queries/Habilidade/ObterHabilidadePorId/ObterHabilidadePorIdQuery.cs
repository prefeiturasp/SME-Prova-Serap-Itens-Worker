using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadePorIdQuery : IRequest<Habilidade>
    {
        public ObterHabilidadePorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}