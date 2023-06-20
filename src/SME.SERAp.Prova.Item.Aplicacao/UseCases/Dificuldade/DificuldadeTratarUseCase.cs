using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Dificuldade;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class DificuldadeTratarUseCase : AbstractUseCase, IDificuldadeTratarUseCase
    {
        public DificuldadeTratarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var dificuldade = mensagemRabbit.ObterObjetoMensagem<DificuldadeDto>();

            if (dificuldade == null) return false;
            if (!dificuldade.Validacao()) return false;

            var dificuldadeAtual = await mediator.Send(new ObterDificuldadePorLegadoIdQuery(dificuldade.Id));

            if (dificuldadeAtual == null)
                return await Inserir(dificuldade);
            else
                return await Alterar(dificuldadeAtual, dificuldade);
        }

        private async Task<bool> Inserir(DificuldadeDto dificuldadeApi)
        {
            var dificuldadeInserir = new Dificuldade(null, dificuldadeApi.Id, dificuldadeApi.Descricao, dificuldadeApi.Ordem, StatusGeral.Ativo);
            await mediator.Send(new InserirDificuldadeCommand(dificuldadeInserir));
            return true;
        }

        private async Task<bool> Alterar(Dificuldade dificuldade, DificuldadeDto dificuldadeApi)
        {
            if (!dificuldade.PossuiAlteracao(dificuldadeApi.Descricao, dificuldadeApi.Ordem, dificuldadeApi.Status))
                return false;

            var dificuldadeAlterar = new Dificuldade(dificuldade.Id, dificuldadeApi.Id, dificuldadeApi.Descricao, dificuldadeApi.Ordem, dificuldadeApi.Status);
            dificuldadeAlterar.CriadoEm = dificuldade.CriadoEm;
            await mediator.Send(new AlterarDificuldadeCommand(dificuldadeAlterar));

            return true;
        }
    }
}
