using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Item.Inserir
{
    public class SalvarItemCommandHandler : IRequestHandler<SalvarItemCommand, long>
    {
        private readonly IServicoClientApi servicoClientApi;
        private const string ROTA_API = "Item/Salvar";

        public SalvarItemCommandHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<long> Handle(SalvarItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var itemLegadoDto = MapearParaLegado(request.Item);

                using var client = servicoClientApi.ObterClientSerapApi();

                var jsonContent = JsonSerializer.Serialize(itemLegadoDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(ROTA_API, content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync(cancellationToken);

                    if (long.TryParse(result, out long itemIdRetornado) && itemIdRetornado > 0)
                    {
                        return itemIdRetornado;
                    }

                    return request.Item.Id ?? 1;
                }

                var erro = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Erro ao salvar item no Legado. Status: {response.StatusCode}. Erro: {erro}");
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao tratar o item para persistência.", ex);
            }
        }

        // --- Mapeamento (ItemDto para ItemApiLegadoDto) ---
        private ItemApiLegadoDto MapearParaLegado(ItemDto itemDtoInterno)
        {
            return new ItemApiLegadoDto
            {
                AreaConhecimentoId = itemDtoInterno.AreaConhecimentoId,
                MatrizId = itemDtoInterno.MatrizId,
                TextoBase = itemDtoInterno.TextoBase,
                Fonte = itemDtoInterno.Fonte,
                CodigoItem = itemDtoInterno.CodigoItem,
                Observacao = itemDtoInterno.Observacao,
                Enunciado = itemDtoInterno.Enunciado,

                CompetenciaId = (int)(itemDtoInterno.CompetenciaId ?? 0),
                HabilidadeId = (int)(itemDtoInterno.HabilidadeId ?? 0),

                TipoItemId = (long)itemDtoInterno.Tipo,

                Dificuldade = itemDtoInterno.DificuldadeSugeridaId.HasValue ?
                                MapearDificuldade(itemDtoInterno.DificuldadeSugeridaId.Value) :
                                DificuldadeEnum.Nenhum,

                TRIDiscrimicacao = itemDtoInterno.Discriminacao,
                TRIAcertoCasual = itemDtoInterno.AcertoCasual,
                TRIDificuldade = itemDtoInterno.Dificuldade,
                PalavrasChave = string.Join(",", itemDtoInterno.PalavrasChave),
                SubassuntoId = itemDtoInterno.SubAssuntoId ?? 0,

                Sigiloso = true, // Não tem essa informação no DTO interno.
                TipoGradeCurricularId = (int)(itemDtoInterno.AnoMatrizId ?? 0),
                Proficiencia = 0, // Não tem essa informação no DTO interno.

                Alternativas = itemDtoInterno.AlternativasDto.Select(a => new AlternativaLegadoDto
                {
                    Descricao = a.Descricao,
                    Correta = a.Correta,
                    Ordem = a.Ordem,
                    Justificativa = a.Justificativa,
                    Numeracao = a.Numeracao
                }).ToList(),

                Audios = itemDtoInterno.Audios?.Select(a => new AudioLegadoDto
                {
                    LegadoId = a.LegadoId,
                    NomeArquivo = a.NomeArquivo,
                    TipoConteudo = a.ContentType,
                    Tamanho = 0,
                    Base64 = null
                }).ToList() ?? new List<AudioLegadoDto>(),

                Videos = itemDtoInterno.Videos?.Select(v => new VideoLegadoDto
                {
                    LegadoId = v.LegadoId,
                    NomeArquivo = v.NomeArquivo,
                    TipoConteudo = v.ContentType,
                    Tamanho = 0,
                    Base64 = null,
                    MiniaturaTamanho = 0,
                    MiniaturaBase64 = null
                }).ToList() ?? new List<VideoLegadoDto>(),

                // Todo: Mapear após descobrir a lógica de imagens no sistema legado
                Imagens = new List<PictureLegadoDto>(),
            };
        }

        private DificuldadeEnum MapearDificuldade(long dificuldadeId)
        {
            switch (dificuldadeId)
            {
                case 1: return DificuldadeEnum.MuitoFacil;
                case 2: return DificuldadeEnum.Facil;
                case 3: return DificuldadeEnum.Medio;
                case 4: return DificuldadeEnum.Dificil;
                case 5: return DificuldadeEnum.MuitoDificil;
                default: return DificuldadeEnum.Nenhum;
            }
        }
    }
}