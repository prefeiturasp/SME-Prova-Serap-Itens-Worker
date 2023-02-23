using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuarioPorLegadoIdQueryHandler : IRequestHandler<ObterUsuarioPorLegadoIdQuery, Usuario>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public ObterUsuarioPorLegadoIdQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<Usuario> Handle(ObterUsuarioPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioUsuario.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
