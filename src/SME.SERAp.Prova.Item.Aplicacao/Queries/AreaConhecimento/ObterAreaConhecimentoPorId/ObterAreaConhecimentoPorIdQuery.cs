using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaConhecimentoPorIdQuery : IRequest<AreaConhecimento>
    {
        public ObterAreaConhecimentoPorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}