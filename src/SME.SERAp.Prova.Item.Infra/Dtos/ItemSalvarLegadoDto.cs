using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemSalvarLegadoDto
    {
        public long ItemId { get; set; }
        public string CodigoItem { get; set; }
        public long AreaConhecimentoId { get; set; }
        public long DisciplinaId { get; set; }
        public long? MatrizId { get; set; }
        public long? CompetenciaId { get; set; }
        public long? HabilidadeId { get; set; }
        public long? AnoMatrizId { get; set; }
        public long? DificuldadeSugeridaId { get; set; }
        public decimal? Discriminacao { get; set; }
        public decimal? AcertoCasual { get; set; }
        public decimal? Dificuldade { get; set; }
        public long? AssuntoId { get; set; }
        public long? SubAssuntoId { get; set; }
        public SituacaoItem Situacao { get; set; }
        public long VersaoItem { get; set; }
        public int? ItemCodeVersion { get; set; }
        public TipoItem Tipo { get; set; }
        public long QuantidadeAlternativasId { get; set; }
        public string[] PalavrasChave { get; set; }
        public decimal? ParametroBTransformado { get; set; }
        public string MediaEhDesvio { get; set; }
        public string Observacao { get; set; }
        public string SentencaDescritora { get; set; }
        public decimal? NivelItem { get; set; }
        public string TextoBase { get; set; }
        public string Fonte { get; set; }
        public string Enunciado { get; set; }
        public List<AlternativaLegadoDto> Alternativas { get; set; }
    }

    public class AlternativaLegadoDto
    {
        public long? Id { get; set; }
        public string Descricao { get; set; }
        public string Justificativa { get; set; }
        public string Numeracao { get; set; }
        public bool Correta { get; set; }
        public int Ordem { get; set; }
    }
}