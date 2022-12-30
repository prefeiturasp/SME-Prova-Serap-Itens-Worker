using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirHabilidadeCommandHandler : IRequestHandler<InserirHabilidadeCommand, long>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public InserirHabilidadeCommandHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<long> Handle(InserirHabilidadeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.IncluirAsync(request.Habilidade);
        }
    }
}
