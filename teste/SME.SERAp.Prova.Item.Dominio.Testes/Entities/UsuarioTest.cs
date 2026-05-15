namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class UsuarioTest
    {
        private readonly Guid _legadoId = Guid.NewGuid();

        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var usuario = new Usuario();
            Assert.Equal(0, usuario.Id);
            Assert.Null(usuario.Login);
            Assert.Null(usuario.Nome);
            Assert.Equal(default, usuario.LegadoId);
        }

        [Fact]
        public void Deve_Criar_Com_Construtor_Parametros()
        {
            var antes = DateTime.Now;
            var usuario = new Usuario(_legadoId, "joao.silva", "João Silva");
            var depois = DateTime.Now;

            Assert.Equal(_legadoId, usuario.LegadoId);
            Assert.Equal("joao.silva", usuario.Login);
            Assert.Equal("João Silva", usuario.Nome);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
            Assert.InRange(usuario.CriadoEm, antes, depois);
            Assert.Null(usuario.AlteradoEm);
        }

        [Fact]
        public void Deve_Alterar_Usuario()
        {
            var usuario = new Usuario(_legadoId, "joao.silva", "João Silva");
            var antes = DateTime.Now;
            usuario.Alterar("joao.santos", "João Santos");
            var depois = DateTime.Now;

            Assert.Equal("joao.santos", usuario.Login);
            Assert.Equal("João Santos", usuario.Nome);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
            Assert.NotNull(usuario.AlteradoEm);
            Assert.InRange(usuario.AlteradoEm!.Value, antes, depois);
        }

        [Fact]
        public void Deve_Inativar_Usuario()
        {
            var usuario = new Usuario(_legadoId, "joao.silva", "João Silva");
            var antes = DateTime.Now;
            usuario.Inativar();
            var depois = DateTime.Now;

            Assert.Equal(StatusGeral.Inativo, usuario.Status);
            Assert.NotNull(usuario.AlteradoEm);
            Assert.InRange(usuario.AlteradoEm!.Value, antes, depois);
        }

        [Fact]
        public void AlteradoEm_Deve_Ser_Nulo_Apos_Criacao()
        {
            var usuario = new Usuario(_legadoId, "joao.silva", "João Silva");
            Assert.Null(usuario.AlteradoEm);
        }

        [Fact]
        public void CriadoEm_Deve_Ter_Apenas_Getter()
        {
            var prop = typeof(Usuario).GetProperty("CriadoEm");
            Assert.NotNull(prop);
            Assert.Null(prop!.SetMethod);
        }

        [Fact]
        public void Deve_Criar_Com_Login_Nulo()
        {
            var usuario = new Usuario(_legadoId, null, "João Silva");
            Assert.Null(usuario.Login);
        }

        [Fact]
        public void Deve_Criar_Com_Nome_Nulo()
        {
            var usuario = new Usuario(_legadoId, "joao.silva", null);
            Assert.Null(usuario.Nome);
        }

        [Fact]
        public void Status_Deve_Ser_Ativo_Apos_Criacao()
        {
            var usuario = new Usuario(_legadoId, "joao.silva", "João Silva");
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }
    }
}