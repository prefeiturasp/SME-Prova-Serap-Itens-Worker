using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosTipoItensQueryHandler : IRequestHandler<ObterTodosTipoItensQuery, IEnumerable<QuantidadeAlternativa>>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioTipoItem;

        public ObterTodosTipoItensQueryHandler(IRepositorioQuantidadeAlternativa repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<IEnumerable<QuantidadeAlternativa>> Handle(ObterTodosTipoItensQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.ObterTudoAsync();
        }
    }
}
