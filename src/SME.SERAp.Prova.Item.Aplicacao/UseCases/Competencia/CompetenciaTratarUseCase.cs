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
            var competencia = mensagemRabbit.ObterObjetoMensagem<CompetenciaDto>();

            if (competencia == null)
                return false;

            if (!competencia.Validacao())
                return false;

            bool retorno;

            var competenciaBase = await mediator.Send(new ObterCompetenciaPorLegadoIdQuery(competencia.Id));

            if (competenciaBase == null)
                retorno = await Inserir(competencia);
            else
                retorno = await Alterar(competenciaBase, competencia);

            if (retorno)
                return await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeSync, competencia.Id));

            return false;
        }

        private async Task<bool> Inserir(CompetenciaDto competencia)
        {
            var competenciaInserir = new Competencia(null, competencia.Codigo, competencia.Id, competencia.MatrizId,
                competencia.Descricao, (int)competencia.Status);

            await mediator.Send(new InserirCompetenciaCommand(competenciaInserir));

            return true;
        }

        private async Task<bool> Alterar(Competencia competenciaBase, CompetenciaDto competencia)
        {
            if (!competenciaBase.PossuiAlteracao(competencia.Codigo, competencia.MatrizId, competencia.Descricao,
                    competencia.Status))
            {
                return true;
            }

            var competenciaAlterar = new Competencia(competenciaBase.Id, competencia.Codigo, competencia.Id,
                competencia.MatrizId, competencia.Descricao, (int)competencia.Status)
            {
                CriadoEm = competenciaBase.CriadoEm
            };

            return await mediator.Send(new AlterarCompetenciaCommand(competenciaAlterar));
        }
    }
}
