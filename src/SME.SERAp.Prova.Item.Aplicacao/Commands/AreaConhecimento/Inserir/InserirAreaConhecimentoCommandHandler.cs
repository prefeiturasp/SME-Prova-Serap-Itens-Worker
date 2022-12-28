using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class InserirAreaConhecimentoCommandHandler : IRequestHandler<InserirAreaConhecimentoCommand, long>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public InserirAreaConhecimentoCommandHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<long> Handle(InserirAreaConhecimentoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.IncluirAsync(request.AreaConhecimento);
        }
    }
}
