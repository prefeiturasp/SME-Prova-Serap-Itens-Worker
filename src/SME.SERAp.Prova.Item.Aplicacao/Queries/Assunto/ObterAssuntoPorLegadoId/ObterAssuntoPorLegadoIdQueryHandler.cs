using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntoPorLegadoIdQueryHandler : IRequestHandler<ObterAssuntoPorLegadoIdQuery, Assunto>
    {

        private readonly IRepositorioAssunto repositorioAssunto;

        public ObterAssuntoPorLegadoIdQueryHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<Assunto> Handle(ObterAssuntoPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAssunto.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
