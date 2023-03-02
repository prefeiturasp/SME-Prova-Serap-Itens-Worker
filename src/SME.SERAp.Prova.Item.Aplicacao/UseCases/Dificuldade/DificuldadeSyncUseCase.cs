using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.Dificuldade;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class DificuldadeSyncUseCase : AbstractUseCase, IDificuldadeSyncUseCase
    {
        public DificuldadeSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var dificuldadesApi = await mediator.Send(new ObterDificuldadesApiSerapQuery());
            if (dificuldadesApi == null || !dificuldadesApi.Any()) return false;

            await Tratar(dificuldadesApi);

            return true;
        }

        private async Task Tratar(List<DificuldadeDto> dificuldadesApi)
        {
            var dificuldadeTratar = dificuldadesApi;

            var dificuldades = await mediator.Send(new ObterTodosDificuldadesQuery());
            var dificuldadesInativar = dificuldades.Where(a => !dificuldadesApi.Any(api => api.Id == a.LegadoId));

            if (dificuldadesInativar != null && dificuldadesInativar.Any())
                dificuldadeTratar.AddRange(dificuldadesInativar.Select(a => new DificuldadeDto(a.LegadoId, a.Descricao, a.Ordem, StatusGeral.Inativo)));

            foreach (var dificuldade in dificuldadeTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DificuldadeTratar, dificuldade));
        }
    }
}
