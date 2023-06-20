using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirAssuntoCommandHandler : IRequestHandler<InserirAssuntoCommand, long>
    {

        private readonly IRepositorioAssunto repositorioAssunto;

        public InserirAssuntoCommandHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<long> Handle(InserirAssuntoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAssunto.IncluirAsync(request.Assunto);
        }
    }
}
