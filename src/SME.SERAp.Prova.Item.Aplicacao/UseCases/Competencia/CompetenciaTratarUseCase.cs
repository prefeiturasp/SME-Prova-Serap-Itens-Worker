using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class CompetenciaTratarUseCase : AbstractUseCase, ICompetenciaTratarUseCase
    {
        public CompetenciaTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var competenciaDto = mensagemRabbit.ObterObjetoMensagem<CompetenciaDto>();

            if (competenciaDto == null) return false;
            if (!competenciaDto.Validacao()) return false;

            var retornoInserirAlterar = true;
            var competenciaBDItem = await mediator.Send(new ObterCompetenciaPorLegadoIdQuery(competenciaDto.Id));
            if (competenciaBDItem == null)
                retornoInserirAlterar = await Inserir(competenciaDto);
            else
                retornoInserirAlterar = await Alterar(competenciaBDItem, competenciaDto);

            if (retornoInserirAlterar)
                return await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeSync, competenciaDto.Id));

            return retornoInserirAlterar;
        }

        private async Task<bool> Inserir(CompetenciaDto dto)
        {
            var competenciaInserir = new Competencia(null, dto.Codigo, dto.Id, dto.MatrizId, dto.Descricao, (int)dto.Status);
            await mediator.Send(new InserirCompetenciaCommand(competenciaInserir));
            return true;
        }

        private async Task<bool> Alterar(Competencia competencia, CompetenciaDto dto)
        {
            if (competencia.PossuiAlteracao(dto.Codigo, dto.MatrizId, dto.Descricao, dto.Status))
            {
                var competenciaAlterar = new Competencia(competencia.Id, dto.Codigo, dto.Id, dto.MatrizId, dto.Descricao, (int)dto.Status);
                competenciaAlterar.CriadoEm = competencia.CriadoEm;
                return await mediator.Send(new AlterarCompetenciaCommand(competenciaAlterar));
            }
            return true;
        }
    }
}
