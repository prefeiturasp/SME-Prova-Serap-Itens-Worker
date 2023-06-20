using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirSubassuntoCommandHandler : IRequestHandler<InserirSubassuntoCommand, long>
    {

        private readonly IRepositorioSubassunto repositorioSubassunto;

        public InserirSubassuntoCommandHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioSubassunto = repositorioSubassunto ?? throw new ArgumentNullException(nameof(repositorioSubassunto));
        }

        public async Task<long> Handle(InserirSubassuntoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioSubassunto.IncluirAsync(request.Subassunto);
        }
    }
}
