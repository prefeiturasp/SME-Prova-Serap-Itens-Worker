namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class GrupoTest
    {
        private readonly Guid _legadoId = Guid.NewGuid();

        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var grupo = new Grupo();
            Assert.Equal(0, grupo.Id);
            Assert.Null(grupo.Nome);
            Assert.Equal(default, grupo.LegadoId);
        }

        [Fact]
        public void Deve_Criar_Com_Construtor_Parametros()
        {
            var antes = DateTime.Now;
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            var depois = DateTime.Now;

            Assert.Equal(_legadoId, grupo.LegadoId);
            Assert.Equal("Administradores", grupo.Nome);
            Assert.True(grupo.PermiteConsultar);
            Assert.True(grupo.PermiteInserir);
            Assert.True(grupo.PermiteAlterar);
            Assert.True(grupo.PermiteExcluir);
            Assert.Equal(StatusGeral.Ativo, grupo.Status);
            Assert.InRange(grupo.CriadoEm, antes, depois);
            Assert.Null(grupo.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Com_Permissoes_False()
        {
            var grupo = new Grupo(_legadoId, "Leitores", true, false, false, false);

            Assert.True(grupo.PermiteConsultar);
            Assert.False(grupo.PermiteInserir);
            Assert.False(grupo.PermiteAlterar);
            Assert.False(grupo.PermiteExcluir);
        }

        [Fact]
        public void Deve_Alterar_Grupo()
        {
            var grupo = new Grupo(_legadoId, "Antigo", true, false, false, false);
            var antes = DateTime.Now;
            grupo.Alterar("Novo Nome", true, true, true, false);
            var depois = DateTime.Now;

            Assert.Equal("Novo Nome", grupo.Nome);
            Assert.True(grupo.PermiteConsultar);
            Assert.True(grupo.PermiteInserir);
            Assert.True(grupo.PermiteAlterar);
            Assert.False(grupo.PermiteExcluir);
            Assert.Equal(StatusGeral.Ativo, grupo.Status);
            Assert.NotNull(grupo.AlteradoEm);
            Assert.InRange(grupo.AlteradoEm!.Value, antes, depois);
        }

        [Fact]
        public void Deve_Inativar_Grupo()
        {
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            var antes = DateTime.Now;
            grupo.Inativar();
            var depois = DateTime.Now;

            Assert.Equal(StatusGeral.Inativo, grupo.Status);
            Assert.NotNull(grupo.AlteradoEm);
            Assert.InRange(grupo.AlteradoEm!.Value, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            Assert.False(grupo.PossuiAlteracao("Administradores", true, true, true, true));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Nome_Diferente()
        {
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            Assert.True(grupo.PossuiAlteracao("Outro Nome", true, true, true, true));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Permissao_Diferente()
        {
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            Assert.True(grupo.PossuiAlteracao("Administradores", true, false, true, true));
        }

        [Fact]
        public void AlteradoEm_Deve_Ser_Nulo_Apos_Criacao()
        {
            var grupo = new Grupo(_legadoId, "Administradores", true, true, true, true);
            Assert.Null(grupo.AlteradoEm);
        }
    }
}