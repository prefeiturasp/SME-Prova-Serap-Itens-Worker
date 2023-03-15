using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AssuntoTratarUseCase : AbstractUseCase, IAssuntoTratarUseCase
    {
        public AssuntoTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            try
            {
                var assunto = mensagemRabbit.ObterObjetoMensagem<AssuntoDto>();

                if (assunto == null)
                    return false;

                if (!assunto.Validacao())
                    return false;

                var assuntoBase = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(assunto.Id));

                bool retorno;

                if (assuntoBase == null)
                    retorno = await Inserir(assunto);
                else
                    retorno = await Alterar(assuntoBase, assunto);

                if (retorno)
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.SubassuntoSync, assunto.Id.ToString()));

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> Inserir(AssuntoDto assunto)
        {
            var assuntoInserir = new Assunto(null, assunto.Id, assunto.DisciplinaId, assunto.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirAssuntoCommand(assuntoInserir));
            return true;
        }

        private async Task<bool> Alterar(Assunto assuntoBase, AssuntoDto assunto)
        {
            if (!assuntoBase.PossuiAlteracao(assunto.Descricao, assunto.Status))
                return true;

            var assuntoAlterar = new Assunto(assuntoBase.Id, assunto.Id, assunto.DisciplinaId, assunto.Descricao, assunto.Status)
            {
                CriadoEm = assuntoBase.CriadoEm
            };

            await mediator.Send(new AlterarAssuntoCommand(assuntoAlterar));

            return true;
        }
    }
}
