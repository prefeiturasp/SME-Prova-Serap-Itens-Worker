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
            var areaConhecimento = mensagemRabbit.ObterObjetoMensagem<AreaConhecimentoDto>();

            if (areaConhecimento == null)
                return false;

            if (!areaConhecimento.Validacao())
                return false;

            var areaConhecimentoLegadoId = areaConhecimento.Id;
            var areaConhecimentoBase = await mediator.Send(new ObterAreaPorLegadoIdQuery(areaConhecimentoLegadoId));

            bool retorno;

            if (areaConhecimentoBase == null)
                retorno = await Inserir(areaConhecimento);
            else
                retorno = await Alterar(areaConhecimentoBase, areaConhecimento);

            if (retorno)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DisciplinaSync,
                    areaConhecimentoLegadoId.ToString()));
            }

            return retorno;
        }

        private async Task<bool> Inserir(AreaConhecimentoDto areaConhecimento)
        {
            var areaConhecimentoDominio = new AreaConhecimento(null, areaConhecimento.Id,
                areaConhecimento.Descricao, StatusGeral.Ativo);

            await mediator.Send(new InserirAreaConhecimentoCommand(areaConhecimentoDominio));

            return true;
        }

        private async Task<bool> Alterar(AreaConhecimento areaConhecimentoBase, AreaConhecimentoDto areaConhecimento)
        {
            if (!areaConhecimentoBase.PossuiAlteracao(areaConhecimento.Descricao, areaConhecimento.Status))
                return true;

            var areaConhecimentoDominio = new AreaConhecimento(areaConhecimentoBase.Id, areaConhecimento.Id,
                areaConhecimento.Descricao, areaConhecimento.Status)
            {
                CriadoEm = areaConhecimentoBase.CriadoEm
            };

            await mediator.Send(new AlterarAreaConhecimentoCommand(areaConhecimentoDominio));

            return true;
        }
    }
}