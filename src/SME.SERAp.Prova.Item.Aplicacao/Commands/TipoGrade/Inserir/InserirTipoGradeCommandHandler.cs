using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTipoGradeCommandHandler : IRequestHandler<InserirTipoGradeCommand, long>
    {

        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public InserirTipoGradeCommandHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<long> Handle(InserirTipoGradeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.IncluirAsync(request.TipoGrade);
        }
    }
}
