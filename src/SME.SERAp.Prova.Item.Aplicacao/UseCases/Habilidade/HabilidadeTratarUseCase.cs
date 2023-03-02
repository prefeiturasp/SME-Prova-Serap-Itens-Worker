using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
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
            var habilidade = mensagemRabbit.ObterObjetoMensagem<Habilidade>();
            
            if (habilidade == null)
                return false;

            var habilidadeBase = await mediator.Send(new ObterHabilidadePorLegadoIdQuery(habilidade.LegadoId));
            
            if (habilidadeBase == null)
            {
                await mediator.Send(new InserirHabilidadeCommand(habilidade));
            }
            else if (habilidadeBase.PossuiAlteracao(habilidade.CompetenciaId, habilidade.Codigo, habilidade.Descricao,
                habilidade.Status))
            {
                if (habilidade.Status == StatusGeral.Inativo)
                    habilidadeBase.Inativar();

                habilidadeBase.Alterar(habilidade.CompetenciaId, habilidade.Codigo, habilidade.Descricao);

                await mediator.Send(new AlterarHabilidadeCommand(habilidadeBase));
            }

            return true;
        }
    }
}
