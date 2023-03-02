namespace SME.SERAp.Prova.Item.Infra.Dtos.UsuarioGrupo
{
    public class UsuarioGrupoDto
    {
        public UsuarioGrupoDto() { }
        public UsuarioGrupoDto(long usuarioId, long grupoId)
        {
            UsuarioId = usuarioId;
            GrupoId = grupoId;
        }

        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
    }
}
