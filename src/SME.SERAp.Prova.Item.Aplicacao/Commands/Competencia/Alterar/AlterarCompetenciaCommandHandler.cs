using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarCompetenciaCommandHandler : IRequestHandler<AlterarCompetenciaCommand, bool>
    {
        private readonly IRepositorioCompetencia repositorioCompetencia;

        public AlterarCompetenciaCommandHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<bool> Handle(AlterarCompetenciaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.UpdateAsync(request.Competencia) > 0;
        }
    }
}
