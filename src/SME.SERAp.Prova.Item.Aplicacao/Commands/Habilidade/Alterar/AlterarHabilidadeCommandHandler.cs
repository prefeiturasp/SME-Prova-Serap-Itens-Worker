using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarHabilidadeCommandHandler : IRequestHandler<AlterarHabilidadeCommand, long>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public AlterarHabilidadeCommandHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<long> Handle(AlterarHabilidadeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.UpdateAsync(request.Habilidade);
        }
    }
}
