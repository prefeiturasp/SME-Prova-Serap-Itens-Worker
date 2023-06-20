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

            if (quantidadeAlternativa == null) 
                return false;
            
            if (!quantidadeAlternativa.Validacao()) 
                return false;

            var quantidadeAlternativaBase = await mediator.Send(new ObterQuantidadeAlternativaPorLegadoIdQuery(quantidadeAlternativa.Id));

            if (quantidadeAlternativaBase == null)
                return await Inserir(quantidadeAlternativa);

            return await Alterar(quantidadeAlternativaBase, quantidadeAlternativa);
        }

        private async Task<bool> Inserir(QuantidadeAlternativaDto quantidadeAlternativa)
        {
            var quantidadeAlternativaInserir = new QuantidadeAlternativa(null, quantidadeAlternativa.Id,
                quantidadeAlternativa.EhPadrao, quantidadeAlternativa.QuantidadeAlternativa,
                quantidadeAlternativa.Descricao, (int)StatusGeral.Ativo);
            
            await mediator.Send(new InserirQuantidadeAlternativaCommand(quantidadeAlternativaInserir));
            return true;
        }

        private async Task<bool> Alterar(QuantidadeAlternativa quantidadeAlternativaBase, QuantidadeAlternativaDto quantidadeAlternativa)
        {
            if (!quantidadeAlternativaBase.PossuiAlteracao(quantidadeAlternativa.EhPadrao,
                quantidadeAlternativa.QuantidadeAlternativa, quantidadeAlternativa.Descricao,
                (int)quantidadeAlternativa.Status))
            {
                return true;
            }

            var quantidadeAlternativaAlterar = new QuantidadeAlternativa(quantidadeAlternativaBase.Id,
                quantidadeAlternativa.Id, quantidadeAlternativa.EhPadrao,
                quantidadeAlternativa.QuantidadeAlternativa, quantidadeAlternativa.Descricao,
                (int)quantidadeAlternativa.Status)
            {
                CriadoEm = quantidadeAlternativaBase.CriadoEm
            };
                
            return await mediator.Send(new AlterarQuantidadeAlternativaCommand(quantidadeAlternativaAlterar));
        }
    }
}
