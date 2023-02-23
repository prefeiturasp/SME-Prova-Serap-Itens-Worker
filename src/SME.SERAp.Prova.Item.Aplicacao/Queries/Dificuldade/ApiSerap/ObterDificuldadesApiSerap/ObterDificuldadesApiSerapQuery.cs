using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Dificuldade;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDificuldadesApiSerapQuery : IRequest<List<DificuldadeDto>>
    {
        public ObterDificuldadesApiSerapQuery() { }
    }
}
