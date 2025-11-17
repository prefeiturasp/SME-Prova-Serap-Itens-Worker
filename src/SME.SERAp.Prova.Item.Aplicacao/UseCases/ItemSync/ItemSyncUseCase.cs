using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Item.Inserir;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item;
using SME.SERAp.Prova.Item.Aplicacao.Queries.ObterAudiosItem;
using SME.SERAp.Prova.Item.Aplicacao.Queries.ObterVideosItem;
using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.ItemSync
{
    public class ItemSyncUseCase : AbstractUseCase, IItemSyncUseCase
    {
        public ItemSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var mensagem = mensagemRabbit.ObterStringMensagem();

            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            if (!long.TryParse(mensagem, out long itemId) || itemId <= 0)
            {
                return false;
            }

            var itemDominio = await mediator.Send(new ObterItemPorIdQuery(itemId));

            if (itemDominio == null)
                return false;

            var alternativasDominio = await mediator.Send(new ObterAlternativasPorItemIdQuery(itemId));

            var audios = await mediator.Send(new ObterAudiosItemQuery(itemId));
            var videos = await mediator.Send(new ObterVideosItemQuery(itemId));

            var itemParaTratar = new ItemDto
            {
                Id = itemDominio.Id,
                CodigoItem = itemDominio.CodigoItem,
                AreaConhecimentoId = itemDominio.AreaconhecimentoId,
                DisciplinaId = itemDominio.DisciplinaId,
                MatrizId = itemDominio.MatrizId ?? 0,
                CompetenciaId = itemDominio.CompetenciaId,
                HabilidadeId = itemDominio.HabilidadeId,
                AnoMatrizId = itemDominio.AnoMatrizId,
                DificuldadeSugeridaId = itemDominio.DificuldadeSugeridaId,
                Discriminacao = itemDominio.Discriminacao,
                AcertoCasual = itemDominio.AcertoCasual,
                Dificuldade = itemDominio.Dificuldade,
                AssuntoId = itemDominio.AssuntoId,
                SubAssuntoId = itemDominio.SubAssuntoId,
                Situacao = itemDominio.Situacao ?? 0,
                Tipo = itemDominio.Tipo ?? 0,
                QuantidadeAlternativasId = itemDominio.QuantidadeAlternativasId ?? 0,
                PalavrasChave = itemDominio.PalavrasChave?.Split(',') ?? System.Array.Empty<string>(),
                ParametroBTransformado = itemDominio.ParametroBTransformado,
                MediaEhDesvio = itemDominio.MediaEhDesvio,
                Observacao = itemDominio.Observacao,
                SentencaDescritora = itemDominio.SentencaDescritora,
                NivelItem = itemDominio.NivelItem,
                TextoBase = itemDominio.TextoBase,
                Fonte = itemDominio.Fonte,
                Enunciado = itemDominio.Enunciado,

                Audios = audios.ToList(),
                Videos = videos.ToList(),

                AlternativasDto = alternativasDominio.Select(a => new AltenativaDto
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    Justificativa = a.Justificativa,
                    Numeracao = a.Numeracao,
                    Correta = a.Correta,
                    Ordem = a.Ordem ?? 0
                }).ToList()
            };

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ItemTratar, itemParaTratar));

            return true;
        }
    }
}