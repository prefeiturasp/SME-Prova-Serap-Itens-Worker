using System;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Usuario
{
    public class UsuarioTratarDto
    {
        public Guid GrupoLegadoId { get; set; }
        public long GrupoId { get; set; }
        public UsuarioCoressoDto UsuarioCoresso { get; set; }
    }
}
