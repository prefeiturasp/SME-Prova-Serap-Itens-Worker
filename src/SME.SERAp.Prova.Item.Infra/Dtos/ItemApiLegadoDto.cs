using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemApiLegadoDto
    {
        public long AreaConhecimentoId { get; set; }
        public long MatrizId { get; set; }
        public string TextoBase { get; set; }
        public string Fonte { get; set; }
        public string CodigoItem { get; set; }
        public int CompetenciaId { get; set; }
        public int HabilidadeId { get; set; }
        public int TipoGradeCurricularId { get; set; }
        public DificuldadeEnum Dificuldade { get; set; }
        public long SubassuntoId { get; set; }
        public string Observacao { get; set; }
        public long TipoItemId { get; set; }
        public bool Sigiloso { get; set; }
        public decimal? TRIDiscrimicacao { get; set; }
        public decimal? TRIAcertoCasual { get; set; }
        public decimal? TRIDificuldade { get; set; }
        public string PalavrasChave { get; set; }
        public int? Proficiencia { get; set; }
        public string Enunciado { get; set; }

        public List<AlternativaLegadoDto> Alternativas { get; set; }
        public List<PictureLegadoDto> Imagens { get; set; }
        public List<VideoLegadoDto> Videos { get; set; }
        public List<AudioLegadoDto> Audios { get; set; }
    }

    public class AlternativaLegadoDto
    {
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public int Ordem { get; set; }
        public string Justificativa { get; set; }
        public string Numeracao { get; set; }
    }

    public class PictureLegadoDto
    {
        public string Tag { get; set; }
        public int Tamanho { get; set; }
        public string TipoConteudo { get; set; }
        public string Base64 { get; set; }
        public string NomeArquivo { get; set; }
        public PictureTypeLegado Tipo { get; set; }
        public long LegadoId { get; set; }
    }

    public class VideoLegadoDto
    {
        public int Tamanho { get; set; }
        public string TipoConteudo { get; set; }
        public string Base64 { get; set; }
        public string NomeArquivo { get; set; }
        public int MiniaturaTamanho { get; set; }
        public string MiniaturaTipoConteudo { get; set; }
        public string MiniaturaBase64 { get; set; }
        public string MiniaturaNomeArquivo { get; set; }
        public long LegadoId { get; set; }
    }

    public class AudioLegadoDto
    {
        public int Tamanho { get; set; }
        public string TipoConteudo { get; set; }
        public string Base64 { get; set; }
        public string NomeArquivo { get; set; }
        public long LegadoId { get; set; }
    }

    public enum PictureTypeLegado
    {
        [Description("Texto_Base")]
        BaseText = 1,
        [Description("Alternativa")]
        Alternative = 2,
        [Description("Justificativa")]
        Justificative = 3,
        [Description("Enunciado")]
        Statement = 4
    }

    public enum DificuldadeEnum
    {
        Nenhum = 0,
        MuitoFacil = 1,
        Facil = 2,
        Medio = 3,
        Dificil = 4,
        MuitoDificil = 5
    }
}