using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class InserirDisciplinaCommandHandler : IRequestHandler<InserirDisciplinaCommand, long>
    {

        private readonly IRepositorioDisciplina repositorioDisciplina;

        public InserirDisciplinaCommandHandler(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina ?? throw new ArgumentNullException(nameof(repositorioDisciplina));
        }

        public async Task<long> Handle(InserirDisciplinaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioDisciplina.IncluirAsync(request.Disciplina);
        }
    }
}