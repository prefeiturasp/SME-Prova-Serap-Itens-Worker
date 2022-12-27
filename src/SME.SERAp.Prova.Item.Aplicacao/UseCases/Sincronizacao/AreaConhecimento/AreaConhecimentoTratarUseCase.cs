using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra;
using System;

using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Sincronizacao.AreaConhecimento
{
    internal class AreaConhecimentoTratarUseCase : AbstractUseCase, IAssuntoTratarUseCase
    {
        public AreaConhecimentoTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
           var areaConhecimentoMensagem = mensagemRabbit.ObterObjetoMensagem<SubassuntoDto>();

            if (areaConhecimentoMensagem == null) return false;
            if (!areaConhecimentoMensagem.Validacao()) return false;

            var areaConhecimentoBase = await mediator.Send(new ObterSubassuntoPorLegadoIdQuery(areaConhecimentoMensagem.Id));

            return true;
           // if (areaConhecimentoBase == null)
           //     return await Inserir(areaConhecimentoMensagem);

         //   return await Alterar(areaConhecimentoBase, areaConhecimentoMensagem);
        }

        private async Task<bool> Inserir(AreaConhecimentoDto areaConhecimento)
        {
            //var assuntoInserir = new AreaConhecimento();// (null, areaConhecimento.Id, areaConhecimento.Descricao, StatusGeral.Ativo);
            //await mediator.Send(new InserirAssuntoCommand(assuntoInserir));
            return true;
        }

        private async Task<bool> Alterar(Assunto assunto, AssuntoDto assuntoApi)
        {
            var assuntoAlterar = new Assunto(assunto.Id, assuntoApi.Id, assuntoApi.Descricao, assuntoApi.Status);
            assuntoAlterar.CriadoEm = assunto.CriadoEm;
            await mediator.Send(new AlterarAssuntoCommand(assuntoAlterar));
            return true;
        }
    }
}