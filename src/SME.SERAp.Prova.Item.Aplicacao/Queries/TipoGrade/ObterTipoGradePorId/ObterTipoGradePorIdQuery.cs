using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorIdQuery : IRequest<TipoGrade>
    {
        public ObterTipoGradePorIdQuery(long id) => Id = id;
        public long Id { get; set; }
    }
}