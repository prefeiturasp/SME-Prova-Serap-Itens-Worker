using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao

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

            var retorno = false;
            if (areaConhecimentoBase == null)
                retorno = await Inserir(areaConhecimentoMensagem);
            else
                retorno = await Alterar(areaConhecimentoBase, areaConhecimentoMensagem);


            if (retorno)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DisciplinaSync, areaConhecimentoMensagem.Id.ToString()));

            return retorno;

        }

        private async Task<bool> Inserir(AreaConhecimentoDto areaConhecimento)
        {
            var areaConhecimentoDominio = new AreaConhecimento(null, areaConhecimento.Id, areaConhecimento.Descricao, StatusGeral.Ativo);
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