using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosPorDisciplinaQueryHandler : IRequestHandler<ObterAssuntosPorDisciplinaQuery, IEnumerable<Assunto>>
    {
        private readonly IRepositorioAssunto repositorioAssunto;

        public ObterAssuntosPorDisciplinaQueryHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<IEnumerable<Assunto>> Handle(ObterAssuntosPorDisciplinaQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAssunto.ObterPorDisciplinaIdAsync(request.DisciplinaId);
        }
    }
}
