using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class TipoGradeTratarUseCase : AbstractUseCase, ITipoGradeTratarUseCase
    {
        public TipoGradeTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var tipoGrade = mensagemRabbit.ObterObjetoMensagem<TipoGradeDto>();

            if (tipoGrade == null)
                return false;

            if (!tipoGrade.Validacao())
                return false;

            var tipoGradeBase = await mediator.Send(new ObterTipoGradePorLegadoIdQuery(tipoGrade.Id));

            if (tipoGradeBase == null)
                return await Inserir(tipoGrade);

            return await Alterar(tipoGradeBase, tipoGrade);
        }

        private async Task<bool> Inserir(TipoGradeDto tipoGrade)
        {
            var tipoGradeInserir = new TipoGrade(null, tipoGrade.Id, tipoGrade.MatrizId, tipoGrade.Descricao,
                tipoGrade.Ordem, tipoGrade.Status);

            await mediator.Send(new InserirTipoGradeCommand(tipoGradeInserir));

            return true;
        }

        private async Task<bool> Alterar(TipoGrade tipoGradeBase, TipoGradeDto tipoGrade)
        {
            if (!tipoGradeBase.PossuiAlteracao(tipoGrade.MatrizId, tipoGrade.Descricao, tipoGrade.Ordem, tipoGrade.Status))
                return true;

            var tipoGradeAlterar =
                new TipoGrade(tipoGradeBase.Id, tipoGrade.Id, tipoGrade.MatrizId, tipoGrade.Descricao, tipoGrade.Ordem,
                    tipoGrade.Status)
                {
                    CriadoEm = tipoGradeBase.CriadoEm
                };

            return await mediator.Send(new AlterarTipoGradeCommand(tipoGradeAlterar));
        }
    }
}
