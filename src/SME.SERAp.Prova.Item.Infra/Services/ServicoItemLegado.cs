using Newtonsoft.Json;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System.Text;

namespace SME.SERAp.Prova.Item.Infra.Services
{
    public class ServicoItemLegado : IServicoItemLegado
    {
        private readonly ClientApiOptions clientApiOptions;
        private readonly HttpClient httpClient;

        public ServicoItemLegado(ClientApiOptions clientApiOptions, HttpClient httpClient)
        {
            this.clientApiOptions = clientApiOptions ?? throw new ArgumentNullException(nameof(clientApiOptions));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<bool> SalvarItemNoLegado(ItemSalvarLegadoDto mensagemDto)
        {
            if (mensagemDto == null)
                throw new ArgumentNullException(nameof(mensagemDto));

            var palavrasChave = mensagemDto.PalavrasChave != null
                ? string.Join(";", mensagemDto.PalavrasChave)
                : string.Empty;

            var payload = new List<object>
            {
                new
                {
                    CodigoItem            = mensagemDto.CodigoItem,
                    ItemVersion           = mensagemDto.VersaoItem > 0 ? (int?)mensagemDto.VersaoItem : null,
                    ItemCodeVersion       = mensagemDto.ItemCodeVersion > 0 ? mensagemDto.ItemCodeVersion : (int?)null,
                    AreaConhecimentoId    = mensagemDto.AreaConhecimentoId,
                    MatrizId              = mensagemDto.MatrizId,
                    TextoBase             = mensagemDto.TextoBase,
                    Fonte                 = mensagemDto.Fonte,
                    CompetenciaId         = mensagemDto.CompetenciaId,
                    HabilidadeId          = mensagemDto.HabilidadeId,
                    TipoGradeCurricularId = mensagemDto.AnoMatrizId,
                    Dificuldade           = mensagemDto.DificuldadeSugeridaId,
                    SubassuntoId          = mensagemDto.SubAssuntoId ?? 0,
                    Observacao            = mensagemDto.Observacao,
                    TipoItemId            = (long)mensagemDto.Tipo,
                    Sigiloso              = false,
                    TRIDiscrimicacao      = mensagemDto.Discriminacao,
                    TRIAcertoCasual       = mensagemDto.AcertoCasual,
                    TRIDificuldade        = mensagemDto.Dificuldade,
                    PalavrasChave         = palavrasChave,
                    Proficiencia          = (int?)null,
                    Enunciado             = mensagemDto.Enunciado,
                    Alternativas          = mensagemDto.Alternativas != null
                        ? mensagemDto.Alternativas.ConvertAll(a => (object)new
                        {
                            Descricao     = a.Descricao,
                            Correta       = a.Correta,
                            Ordem         = a.Ordem,
                            Justificativa = a.Justificativa,
                            Numeracao     = a.Numeracao
                        })
                        : new List<object>(),
                    Imagens = new List<object>(),
                    Videos  = new List<object>(),
                    Audios  = new List<object>()
                }
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("keyApi", clientApiOptions.LegadoApiChave);

            var url = $"{clientApiOptions.LegadoApiUrlBase}/api/Item/Salvar";
            var response = await httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao salvar item no legado. Status: {response.StatusCode}. Detalhe: {erro}");
            }

            return true;
        }
    }
}