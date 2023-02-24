using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class QuantidadeAlternativaSyncUseCase : AbstractUseCase, IQuantidadeAlternativaSyncUseCase
    {
        public QuantidadeAlternativaSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var qtdAlternativasApi = await mediator.Send(new ObterQuantidadeAlternativaApiSerapQuery());
            if (qtdAlternativasApi == null || !qtdAlternativasApi.Any()) return false;

            await Tratar(qtdAlternativasApi);

            return true;
        }

        private async Task Tratar(List<QuantidadeAlternativaDto> tipoItensApi)
        {
            var tipoItensTratar = tipoItensApi;

            var tipoItensItens = await mediator.Send(new ObterTodasQuantidadesAlternativasQuery());
            var tipoItensInativar = tipoItensItens.Where(a => !tipoItensApi.Any(api => api.Id == a.LegadoId));

            if (tipoItensInativar != null && tipoItensInativar.Any())
                tipoItensTratar.AddRange(tipoItensInativar.Select(a => new QuantidadeAlternativaDto(a.LegadoId, a.EhPadrao, a.QtdeAlternativa, a.Descricao, StatusGeral.Inativo)));

            foreach (var quantidadeAlternativa in tipoItensTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.QuantidadeAlternativaTratar, quantidadeAlternativa));
        }
    }
}
