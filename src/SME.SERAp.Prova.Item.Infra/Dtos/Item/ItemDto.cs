using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Item
{
    public class ItemDto
    {
        public long? Id { get; set; }
        public string CodigoItem { get; set; }
        public long AreaConhecimentoId { get; set; }
        public long DisciplinaId { get; set; }
        public long MatrizId { get; set; }
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
        public List<AltenativaDto> AlternativasDto { get; set; }
        public long ArquivoVideoId { get; set; }
        public long ArquivoAudioId { get; set; }
        public List<ItemArquivoDto> Audios { get; set; }
        public List<ItemArquivoDto> Videos { get; set; }
    }
}