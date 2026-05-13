using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("habilidade")]
    public class Habilidade : EntidadeBase
    {
        public Habilidade() { }

        public Habilidade(long legadoId, long competenciaId, string codigo, string descricao)
        {
            LegadoId = legadoId;
            CompetenciaId = competenciaId;
            Codigo = codigo;
            Descricao = descricao;
            CriadoEm = AlteradoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("competencia_id")]
        public long CompetenciaId { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public StatusGeral Status { get; set; }

        public void Alterar(long competenciaId, string codigo, string descricao)
        {
            CompetenciaId = competenciaId;
            Codigo = codigo;
            Descricao = descricao;
            AlteradoEm = DateTime.Now;
        }

        public void Inativar() => Status = StatusGeral.Inativo;

        public bool PossuiAlteracao(long competenciaId, string codigo, string descricao, StatusGeral status)
            => CompetenciaId != competenciaId || Codigo != codigo || Descricao != descricao || Status != status;
    }
}