using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("competencia")]
    public class Competencia : EntidadeBase
    {
        public Competencia() { }

        public Competencia(long? id, string codigo, long legadoId, long matrizId, string descricao, int status)
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

            Codigo = codigo;
            LegadoId = legadoId;
            MatrizId = matrizId;
            Descricao = descricao;
            Status = status;
        }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("matriz_id")]
        public long MatrizId { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public int Status { get; set; }

        public bool PossuiAlteracao(string codigo, long matrizId, string descricao, StatusGeral status)
            => Codigo != codigo || MatrizId != matrizId || Descricao != descricao || Status != (int)status;
    }
}