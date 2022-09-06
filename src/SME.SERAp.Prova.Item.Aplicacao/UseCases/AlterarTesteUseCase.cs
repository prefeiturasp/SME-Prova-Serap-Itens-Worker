using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class AlterarTesteUseCase : AbstractUseCase, IAlterarTesteUseCase
    {
        public AlterarTesteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var testeDto = mensagemRabbit.ObterObjetoMensagem<TesteDto>();
            if (testeDto == null) return false;
            return await mediator.Send(new AlterarTesteCommand(testeDto));
        }
    }
}
