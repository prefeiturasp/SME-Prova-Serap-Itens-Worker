using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarUsuarioCommandHandler : IRequestHandler<AlterarUsuarioCommand, long>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public AlterarUsuarioCommandHandler(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public Task<long> Handle(AlterarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return repositorioUsuario.UpdateAsync(request.Usuario);
        }
    }
}
