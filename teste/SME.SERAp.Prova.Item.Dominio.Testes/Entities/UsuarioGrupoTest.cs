namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class UsuarioGrupoTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var ug = new UsuarioGrupo();
            Assert.Equal(0, ug.Id);
            Assert.Equal(0, ug.UsuarioId);
            Assert.Equal(0, ug.GrupoId);
        }

        [Fact]
        public void Deve_Criar_Com_Construtor_Parametros()
        {
            var antes = DateTime.Now;
            var ug = new UsuarioGrupo(10, 3);
            var depois = DateTime.Now;

            Assert.Equal(10, ug.UsuarioId);
            Assert.Equal(3, ug.GrupoId);
            Assert.Equal(StatusGeral.Ativo, ug.Status);
            Assert.InRange(ug.CriadoEm, antes, depois);
            Assert.Null(ug.AlteradoEm);
        }

        [Fact]
        public void Deve_Inativar_UsuarioGrupo()
        {
            var ug = new UsuarioGrupo(10, 3);
            var antes = DateTime.Now;
            ug.Inativar();
            var depois = DateTime.Now;

            Assert.Equal(StatusGeral.Inativo, ug.Status);
            Assert.NotNull(ug.AlteradoEm);
            Assert.InRange(ug.AlteradoEm!.Value, antes, depois);
        }

        [Fact]
        public void AlteradoEm_Deve_Ser_Nulo_Apos_Criacao()
        {
            var ug = new UsuarioGrupo(10, 3);
            Assert.Null(ug.AlteradoEm);
        }

        [Fact]
        public void Status_Deve_Ser_Ativo_Apos_Criacao()
        {
            var ug = new UsuarioGrupo(10, 3);
            Assert.Equal(StatusGeral.Ativo, ug.Status);
        }

        [Fact]
        public void Deve_Criar_Com_UsuarioId_Maximo()
        {
            var ug = new UsuarioGrupo(long.MaxValue, 1);
            Assert.Equal(long.MaxValue, ug.UsuarioId);
        }

        [Fact]
        public void Deve_Criar_Com_GrupoId_Maximo()
        {
            var ug = new UsuarioGrupo(1, long.MaxValue);
            Assert.Equal(long.MaxValue, ug.GrupoId);
        }

        [Fact]
        public void Deve_Herdar_Id_De_EntidadeBase()
        {
            var ug = new UsuarioGrupo();
            ug.Id = 77;
            Assert.Equal(77, ug.Id);
        }
    }
}