using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Disciplina.ApiSerap;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Disciplina
{
    public class DisciplinaSyncUseCase : AbstractUseCase, IDisciplinaSyncUseCase
    {
        public DisciplinaSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var disciplinaApi = await mediator.Send(new ObterDisciplinaPorAreaConhecimentoIdQuery());
            if (disciplinaApi == null || !disciplinaApi.Any()) return false;

            await Tratar(disciplinaApi);

            return true;
        }

        private async Task Tratar(IEnumerable<DisciplinaDto> listaDisciplinaDto)
        {
            var disciplinaTratar = listaDisciplinaDto.ToList();

            var disciplinasBase = await mediator.Send(new ObterTodasDisciplinasQuery());
            var disciplinasInativar = disciplinasBase.Where(a => !listaDisciplinaDto.Any(api => api.Id == a.LegadoId));

            if (disciplinasInativar != null && disciplinasInativar.Any())
                disciplinaTratar.AddRange(disciplinasInativar.Select(a => new DisciplinaDto(a.LegadoId, a.Descricao, StatusGeral.Inativo)));

            foreach (var disciplina in disciplinaTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DisciplinaTratar, disciplina));
        }
    }
}