using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarAreaConhecimentoCommandHandler : IRequestHandler<AlterarAreaConhecimentoCommand, long>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public AlterarAreaConhecimentoCommandHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<long> Handle(AlterarAreaConhecimentoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.UpdateAsync(request.AreaConhecimento);
        }
    }
}