using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirCompetenciaCommandHandler : IRequestHandler<InserirCompetenciaCommand, long>
    {

        private readonly IRepositorioCompetencia repositorioCompetencia;

        public InserirCompetenciaCommandHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<long> Handle(InserirCompetenciaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.IncluirAsync(request.Competencia);
        }
    }
}
