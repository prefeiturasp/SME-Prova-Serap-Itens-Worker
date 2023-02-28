using System;

namespace SME.SERAp.Prova.Item.Dominio
{
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

        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
        public StatusGeral Status { get; set; }
    }
}
