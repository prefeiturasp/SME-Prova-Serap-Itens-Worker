using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodasQuantidadesAlternativasQueryHandler : IRequestHandler<ObterTodasQuantidadesAlternativasQuery, IEnumerable<QuantidadeAlternativa>>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativas;

        public ObterTodasQuantidadesAlternativasQueryHandler(IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativas)
        {
            this.repositorioQuantidadeAlternativas = repositorioQuantidadeAlternativas ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativas));
        }

        public async Task<IEnumerable<QuantidadeAlternativa>> Handle(ObterTodasQuantidadesAlternativasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativas.ObterTudoAsync();
        }
    }
}
