using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AssuntoSyncUseCase : AbstractUseCase, IAssuntoSyncUseCase
    {
        public AssuntoSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            try
            {
                if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                    return false;

                var disciplinaLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

                var assuntosApi = await mediator.Send(new ObterAssuntosApiSerapQuery(disciplinaLegadoId));

                if (assuntosApi == null || !assuntosApi.Any())
                    return false;

                var disciplinaBase = await mediator.Send(new ObterDisciplinaPorLegadoIdQuery(disciplinaLegadoId));

                if (disciplinaBase == null)
                    return false;

                await Tratar(assuntosApi, disciplinaBase.Id);

                return true;
            }
            catch(Exception e)
            {
                throw e;
            }            
        }

        private async Task Tratar(IEnumerable<AssuntoDto> assuntosApi, long disciplinaId)
        {
            var assuntosTratar = new List<AssuntoDto>();
            assuntosTratar.AddRange(assuntosApi);

            var assuntosItens = await mediator.Send(new ObterAssuntosPorDisciplinaQuery(disciplinaId));
            var assuntosInativar = assuntosItens.Where(a => assuntosTratar.All(api => api.Id != a.LegadoId));

            if (assuntosInativar.Any())
            {
                assuntosTratar.AddRange(assuntosInativar.Select(a =>
                        new AssuntoDto(a.LegadoId, a.Descricao, StatusGeral.Inativo, disciplinaId))
                    .Except(assuntosTratar));
            }

            foreach (var assunto in assuntosTratar)
            {
                assunto.DisciplinaId = disciplinaId;
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AssuntoTratar, assunto));
            }
        }
    }
}
