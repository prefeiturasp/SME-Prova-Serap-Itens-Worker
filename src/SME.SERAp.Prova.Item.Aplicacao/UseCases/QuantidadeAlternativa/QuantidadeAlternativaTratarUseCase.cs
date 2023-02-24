using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class QuantidadeAlternativaTratarUseCase : AbstractUseCase, IQuantidadeAlternativaTratarUseCase
    {
        public QuantidadeAlternativaTratarUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var quantidadeAlternativa = mensagemRabbit.ObterObjetoMensagem<QuantidadeAlternativaDto>();

            if (quantidadeAlternativa == null) return false;
            if (!quantidadeAlternativa.Validacao()) return false;

            var quantidadeAlternativaAtual = await mediator.Send(new ObterQuantidadeAlternativaPorLegadoIdQuery(quantidadeAlternativa.Id));

            if (quantidadeAlternativaAtual == null)
                return await Inserir(quantidadeAlternativa);

            return await Alterar(quantidadeAlternativaAtual, quantidadeAlternativa);
        }

        private async Task<bool> Inserir(QuantidadeAlternativaDto quantidadeAlternativaApi)
        {
            var quantidadeAlternativaInserir = new QuantidadeAlternativa(null, quantidadeAlternativaApi.Id, quantidadeAlternativaApi.EhPadrao, quantidadeAlternativaApi.QuantidadeAlternativa, quantidadeAlternativaApi.Descricao, (int)StatusGeral.Ativo);
            await mediator.Send(new InserirQuantidadeAlternativaCommand(quantidadeAlternativaInserir));
            return true;
        }

        private async Task<bool> Alterar(QuantidadeAlternativa quantidadeAlternativa, QuantidadeAlternativaDto quantidadeAlternativaApi)
        {
            if (quantidadeAlternativa.PossuiAlteracao(quantidadeAlternativaApi.EhPadrao, quantidadeAlternativaApi.QuantidadeAlternativa, quantidadeAlternativaApi.Descricao, (int)quantidadeAlternativaApi.Status))
            {
                var quantidadeAlternativaAlterar = new QuantidadeAlternativa(quantidadeAlternativa.Id, quantidadeAlternativaApi.Id, quantidadeAlternativaApi.EhPadrao, quantidadeAlternativaApi.QuantidadeAlternativa, quantidadeAlternativaApi.Descricao, (int)quantidadeAlternativaApi.Status);
                quantidadeAlternativaAlterar.CriadoEm = quantidadeAlternativa.CriadoEm;
                return await mediator.Send(new AlterarQuantidadeAlternativaCommand(quantidadeAlternativaAlterar));
            }
            return true;
        }
    }
}
