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
            var tipoGradeDto = mensagemRabbit.ObterObjetoMensagem<TipoGradeDto>();

            if (tipoGradeDto == null) return false;
            if (!tipoGradeDto.Validacao()) return false;

            var tipoGradeBDItem = await mediator.Send(new ObterTipoGradePorLegadoIdQuery(tipoGradeDto.Id));
            if (tipoGradeBDItem == null)
                return await Inserir(tipoGradeDto);

            return await Alterar(tipoGradeBDItem, tipoGradeDto);
        }

        private async Task<bool> Inserir(TipoGradeDto dto)
        {
            var tipoGradeInserir = new TipoGrade(null, dto.Id, dto.MatrizId, dto.Descricao, dto.Ordem, dto.Status);
            await mediator.Send(new InserirTipoGradeCommand(tipoGradeInserir));
            return true;
        }

        private async Task<bool> Alterar(TipoGrade tipoGrade, TipoGradeDto dto)
        {
            if (tipoGrade.PossuiAlteracao(dto.MatrizId, dto.Descricao, dto.Ordem, dto.Status))
            {
                var tipoGradeAlterar = new TipoGrade(tipoGrade.Id, dto.Id, dto.MatrizId, dto.Descricao, dto.Ordem, dto.Status);
                tipoGradeAlterar.CriadoEm = tipoGrade.CriadoEm;
                return await mediator.Send(new AlterarTipoGradeCommand(tipoGradeAlterar));
            }
            return true;
        }

    }
}
