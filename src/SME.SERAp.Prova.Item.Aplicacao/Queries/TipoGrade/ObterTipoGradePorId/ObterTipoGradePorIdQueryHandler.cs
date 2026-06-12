using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorIdQueryHandler : IRequestHandler<ObterTipoGradePorIdQuery, TipoGrade>
    {
        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public ObterTipoGradePorIdQueryHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<TipoGrade> Handle(ObterTipoGradePorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.ObterPorIdAsync(request.Id);
        }
    }
}