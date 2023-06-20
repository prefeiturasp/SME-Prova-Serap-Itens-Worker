using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarAssuntoCommandHandler : IRequestHandler<AlterarAssuntoCommand, bool>
    {
        private readonly IRepositorioAssunto repositorioAssunto;

        public AlterarAssuntoCommandHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<bool> Handle(AlterarAssuntoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAssunto.UpdateAsync(request.Assunto) > 0;
        }
    }
}
