using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class HabilidadeTratarUseCase : AbstractUseCase, IHabilidadeTratarUseCase
    {
        public HabilidadeTratarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var habilidade = mensagemRabbit.ObterObjetoMensagem<Dominio.Entities.Habilidade>();
            if (habilidade == null) return false;

            var habilidadeBanco = await mediator.Send(new ObterHabilidadePorLegadoIdQuery(habilidade.LegadoId));
            if (habilidadeBanco == null)
            {
                await mediator.Send(new InserirHabilidadeCommand(habilidade));
            }
            else if (habilidade.CompetenciaId != habilidadeBanco.CompetenciaId ||
                habilidade.Codigo != habilidadeBanco.Codigo ||
                habilidade.Descricao != habilidadeBanco.Descricao ||
                habilidade.Status != habilidadeBanco.Status)
            {
                if (habilidade.Status == Dominio.StatusGeral.Inativo)
                    habilidadeBanco.Inativar();

                habilidadeBanco.Alterar(habilidade.CompetenciaId, habilidade.Codigo, habilidade.Descricao);

                await mediator.Send(new AlterarHabilidadeCommand(habilidadeBanco));
            }

            return true;
        }
    }
}
