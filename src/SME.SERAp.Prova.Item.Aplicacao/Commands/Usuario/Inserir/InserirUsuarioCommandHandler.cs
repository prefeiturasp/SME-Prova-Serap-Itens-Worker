using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirUsuarioCommandHandler : IRequestHandler<InserirUsuarioCommand, long>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public InserirUsuarioCommandHandler(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<long> Handle(InserirUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await repositorioUsuario.IncluirAsync(request.Usuario);
        }
    }
}
