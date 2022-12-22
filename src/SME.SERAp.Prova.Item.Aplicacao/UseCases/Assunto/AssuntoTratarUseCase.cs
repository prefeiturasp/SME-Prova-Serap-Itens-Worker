using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AssuntoTratarUseCase : AbstractUseCase, IAssuntoTratarUseCase
    {
        public AssuntoTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var assunto = mensagemRabbit.ObterObjetoMensagem<AssuntoDto>();
            if (assunto == null) return false;

            var assuntoAtual = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(assunto.Id));

            if (assuntoAtual == null)
                return await Inserir(assunto);

            return await Alterar(assuntoAtual.Id, assunto);
        }

        private async Task<bool> Inserir(AssuntoDto assuntoApi)
        {
            var assuntoInserir = new Assunto(null, assuntoApi.Id, assuntoApi.Descricao, 1);
            await mediator.Send(new InserirAssuntoCommand(assuntoInserir));
            return true;
        }

        private async Task<bool> Alterar(long id, AssuntoDto assuntoApi)
        {
            var assuntoAlterar = new Assunto(id, assuntoApi.Id, assuntoApi.Descricao, 1);
            await mediator.Send(new AlterarAssuntoCommand(assuntoAlterar));
            return true;
        }
    }
}
