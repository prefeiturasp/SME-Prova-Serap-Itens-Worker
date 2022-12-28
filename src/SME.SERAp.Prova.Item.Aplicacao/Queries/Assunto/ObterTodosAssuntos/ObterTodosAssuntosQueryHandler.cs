using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosAssuntosQueryHandler : IRequestHandler<ObterTodosAssuntosQuery, IEnumerable<Assunto>>
    {

        private readonly IRepositorioAssunto repositorioAssunto;

        public ObterTodosAssuntosQueryHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<IEnumerable<Assunto>> Handle(ObterTodosAssuntosQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAssunto.ObterTudoAsync();
        }
    }
}
