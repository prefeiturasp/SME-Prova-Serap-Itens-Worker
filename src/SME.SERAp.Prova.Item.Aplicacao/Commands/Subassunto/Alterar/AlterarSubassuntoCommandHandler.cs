using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarSubassuntoCommandHandler : IRequestHandler<AlterarSubassuntoCommand, bool>
    {
        private readonly IRepositorioSubassunto repositorioSubassunto;

        public AlterarSubassuntoCommandHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioSubassunto = repositorioSubassunto ?? throw new ArgumentNullException(nameof(repositorioSubassunto));
        }

        public async Task<bool> Handle(AlterarSubassuntoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioSubassunto.UpdateAsync(request.Subassunto) > 0;
        }
    }
}
