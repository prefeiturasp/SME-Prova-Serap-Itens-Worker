using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodasQuantiadeAlternativasQueryHandler : IRequestHandler<ObterTodosTipoItensQuery, IEnumerable<QuantidadeAlternativa>>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativas;

        public ObterTodasQuantiadeAlternativasQueryHandler(IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativas)
        {
            this.repositorioQuantidadeAlternativas = repositorioQuantidadeAlternativas ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativas));
        }

        public async Task<IEnumerable<QuantidadeAlternativa>> Handle(ObterTodosTipoItensQuery request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativas.ObterTudoAsync();
        }
    }
}
