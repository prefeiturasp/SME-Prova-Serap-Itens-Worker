using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{ 
    internal class AlterarAreaConhecimentoCommandHandler : IRequestHandler<InserirAreaConhecimentoCommand, long>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public AlterarAreaConhecimentoCommandHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<long> Handle(InserirAreaConhecimentoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.UpdateAsync(request.AreaConhecimento);
        }
    }
}