using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosAssuntosQuery : IRequest<IEnumerable<Assunto>>
    {
        public ObterTodosAssuntosQuery()
        {

        }
    }
}
