using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("usuario_grupo")]
    public class UsuarioGrupo : EntidadeBase
    {
        public UsuarioGrupo() { }

        public UsuarioGrupo(long usuarioId, long grupoId)
        {
            UsuarioId = usuarioId;
            GrupoId = grupoId;
            CriadoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Inativar()
        {
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Inativo;
        }

        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        [Column("grupo_id")]
        public long GrupoId { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }

        [Column("status")]
        public StatusGeral Status { get; set; }
    }
}