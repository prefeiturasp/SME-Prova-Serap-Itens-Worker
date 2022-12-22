using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntosPorAssuntoIdQueryHandler : IRequestHandler<ObterSubassuntosPorAssuntoIdQuery, IEnumerable<Subassunto>>
    {

        private readonly IRepositorioSubassunto repositorioSubassunto;

        public ObterSubassuntosPorAssuntoIdQueryHandler(IRepositorioSubassunto repositorioSubassunto)
        {
            this.repositorioSubassunto = repositorioSubassunto ?? throw new ArgumentNullException(nameof(repositorioSubassunto));
        }

        public async Task<IEnumerable<Subassunto>> Handle(ObterSubassuntosPorAssuntoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSubassunto.ObterPorAssuntoIdAsync(request.AssuntoId);
        }
    }
}
