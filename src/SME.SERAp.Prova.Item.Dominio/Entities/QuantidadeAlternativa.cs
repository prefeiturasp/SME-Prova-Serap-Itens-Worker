using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("quantidade_alternativa")]
    public class QuantidadeAlternativa : EntidadeBase
    {
        public QuantidadeAlternativa() { }

        public QuantidadeAlternativa(long? id, long legadoId, bool ehPadrao, int qtdeAlternativa, string descricao, int status)
        {
            if (id == null)
            {
                CriadoEm = AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;
            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            EhPadrao = ehPadrao;
            QtdeAlternativa = qtdeAlternativa;
            Descricao = descricao;
            Status = status;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("eh_padrao")]
        public bool EhPadrao { get; set; }

        [Column("qtde_alternativa")]
        public int QtdeAlternativa { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public int Status { get; set; }

        public bool PossuiAlteracao(bool ehPadrao, int qtdeAlternativa, string descricao, int status)
            => EhPadrao != ehPadrao || QtdeAlternativa != qtdeAlternativa || Descricao != descricao || Status != status;
    }
}