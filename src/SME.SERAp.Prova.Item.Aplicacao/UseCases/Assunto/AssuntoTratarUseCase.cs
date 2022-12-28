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
            if (!assunto.Validacao()) return false;

            var assuntoAtual = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(assunto.Id));

            var retAssunto = true;
            if (assuntoAtual == null)
                retAssunto = await Inserir(assunto);
            else
                retAssunto = await Alterar(assuntoAtual, assunto);

            if (retAssunto)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.SubassuntoSync, assunto.Id.ToString()));

            return retAssunto;
        }

        private async Task<bool> Inserir(AssuntoDto assuntoApi)
        {
            var assuntoInserir = new Assunto(null, assuntoApi.Id, assuntoApi.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirAssuntoCommand(assuntoInserir));
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
