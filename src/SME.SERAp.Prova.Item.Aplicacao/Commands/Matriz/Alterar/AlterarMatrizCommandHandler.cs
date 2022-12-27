using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{ 
    public class AlterarMatrizCommandHandler : IRequestHandler<AlterarMatrizCommand, bool>
    {
        private readonly IRepositorioMatriz repositorioMatriz;

        public AlterarMatrizCommandHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioMatriz = repositorioMatriz ?? throw new ArgumentNullException(nameof(repositorioMatriz));
        }

        public async Task<bool> Handle(AlterarMatrizCommand request, CancellationToken cancellationToken)
        {
            return await repositorioMatriz.UpdateAsync(request.Matriz) > 0;
        }
    }
}
