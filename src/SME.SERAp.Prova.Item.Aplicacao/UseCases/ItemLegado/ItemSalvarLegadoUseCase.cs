using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.ItemLegado
{
    public class ItemSalvarLegadoUseCase : IItemSalvarLegadoUseCase
    {
        private readonly IServicoItemLegado servicoItemLegado;
        private readonly IMediator mediator;

        public ItemSalvarLegadoUseCase(IServicoItemLegado servicoItemLegado, IMediator mediator)
        {
            this.servicoItemLegado = servicoItemLegado ?? throw new ArgumentNullException(nameof(servicoItemLegado));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Executar(MensagemRabbit mensagem)
        {
            var mensagemDto = mensagem.ObterObjetoMensagem<ItemSalvarLegadoMensagemDto>();

            if (mensagemDto?.ItemDto == null)
                throw new Exception("Mensagem inválida para sincronização com o legado.");

            var itemSalvarLegadoDto = await MapearParaLegadoDto(mensagemDto);

            return await servicoItemLegado.SalvarItemNoLegado(itemSalvarLegadoDto);
        }

        private async Task<ItemSalvarLegadoDto> MapearParaLegadoDto(ItemSalvarLegadoMensagemDto mensagemDto)
        {
            var dto = mensagemDto.ItemDto;

            var matriz = await mediator.Send(new ObterMatrizPorIdQuery(dto.MatrizId));
            var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorIdQuery(dto.AreaConhecimentoId));
            var competencia = dto.CompetenciaId.HasValue ? await mediator.Send(new ObterCompetenciaPorIdQuery(dto.CompetenciaId.Value)) : null;
            var habilidade = dto.HabilidadeId.HasValue ? await mediator.Send(new ObterHabilidadePorIdQuery(dto.HabilidadeId.Value)) : null;
            var dificuldade = dto.DificuldadeSugeridaId.HasValue ? await mediator.Send(new ObterDificuldadePorIdQuery(dto.DificuldadeSugeridaId.Value)) : null;
            var subassunto = dto.SubAssuntoId.HasValue ? await mediator.Send(new ObterSubassuntoPorIdQuery(dto.SubAssuntoId.Value)) : null;
            var tipoGrade = dto.AnoMatrizId.HasValue ? await mediator.Send(new ObterTipoGradePorIdQuery(dto.AnoMatrizId.Value)) : null;

            return new ItemSalvarLegadoDto
            {
                ItemId = mensagemDto.ItemId,
                CodigoItem = dto.CodigoItem,
                AreaConhecimentoId = areaConhecimento?.LegadoId ?? dto.AreaConhecimentoId,
                DisciplinaId = dto.DisciplinaId,
                MatrizId = matriz?.LegadoId ?? dto.MatrizId,
                CompetenciaId = competencia?.LegadoId,
                HabilidadeId = habilidade?.LegadoId,
                AnoMatrizId = tipoGrade?.LegadoId,
                DificuldadeSugeridaId = dificuldade?.LegadoId,
                Discriminacao = dto.Discriminacao,
                AcertoCasual = dto.AcertoCasual,
                Dificuldade = dto.Dificuldade,
                AssuntoId = dto.AssuntoId,
                SubAssuntoId = subassunto?.LegadoId,
                Situacao = dto.Situacao,
                Tipo = dto.Tipo,
                QuantidadeAlternativasId = dto.QuantidadeAlternativasId,
                PalavrasChave = dto.PalavrasChave,
                ParametroBTransformado = dto.ParametroBTransformado,
                MediaEhDesvio = dto.MediaEhDesvio,
                Observacao = dto.Observacao,
                SentencaDescritora = dto.SentencaDescritora,
                NivelItem = dto.NivelItem,
                VersaoItem = dto.VersaoItem,
                ItemCodeVersion = dto.ItemCodeVersion,
                TextoBase = dto.TextoBase,
                Fonte = dto.Fonte,
                Enunciado = dto.Enunciado,
                Alternativas = dto.AlternativasDto?.ConvertAll(a => new AlternativaLegadoDto
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    Justificativa = a.Justificativa,
                    Numeracao = a.Numeracao,
                    Correta = a.Correta,
                    Ordem = a.Ordem
                })
            };
        }
    }
}