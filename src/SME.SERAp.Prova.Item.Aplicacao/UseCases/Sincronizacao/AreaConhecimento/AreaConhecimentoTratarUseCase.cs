using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra;
using System;

using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class AreaConhecimentoTratarUseCase : AbstractUseCase, IAreaConhecimentoUseCase
    {
        public AreaConhecimentoTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
           var areaConhecimentoMensagem = mensagemRabbit.ObterObjetoMensagem<AreaConhecimentoDto>();

            if (areaConhecimentoMensagem == null) return false;
            if (!areaConhecimentoMensagem.Validacao()) return false;

            var areaConhecimentoBase = await mediator.Send(new ObterAreaPorLegadoIdQuery(areaConhecimentoMensagem.Id));

           
            if (areaConhecimentoBase == null)
                return await Inserir(areaConhecimentoMensagem);

            return await Alterar(areaConhecimentoBase, areaConhecimentoMensagem);
        }

        private async Task<bool> Inserir(AreaConhecimentoDto areaConhecimento)
        {
            var areaConhecimentoDominio = new   AreaConhecimento(null, areaConhecimento.Id, areaConhecimento.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirAreaConhecimentoCommand(areaConhecimentoDominio));
            return true;
        }

        private async Task<bool> Alterar(AreaConhecimento areaConhecimentoBase, AreaConhecimentoDto assuntoApi)
        {
            var areaConhecimentoDominio = new AreaConhecimento(areaConhecimentoBase.Id, assuntoApi.Id, assuntoApi.Descricao, assuntoApi.Status);
            areaConhecimentoDominio.CriadoEm = areaConhecimentoBase.CriadoEm;
            await mediator.Send(new AlterarAreaConhecimentoCommand(areaConhecimentoDominio));
            return true;
        }
    }
}