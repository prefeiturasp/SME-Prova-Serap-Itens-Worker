using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarQuantidadeAlternativaCommandHandler : IRequestHandler<AlterarQuantidadeAlternativaCommand, bool>
    {
        private readonly IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa;

        public AlterarQuantidadeAlternativaCommandHandler(IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa)
        {
            this.repositorioQuantidadeAlternativa = repositorioQuantidadeAlternativa ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativa));
        }

        public async Task<bool> Handle(AlterarQuantidadeAlternativaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativa.UpdateAsync(request.TipoItem) > 0;
        }
    }
}
