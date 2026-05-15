namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class TipoGradeTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var tg = new TipoGrade();
            Assert.Equal(0, tg.Id);
            Assert.Null(tg.Descricao);
            Assert.Equal(0, tg.Ordem);
        }

        [Fact]
        public void Deve_Criar_Novo_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var tg = new TipoGrade(null, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, tg.Id);
            Assert.Equal(8, tg.LegadoId);
            Assert.Equal(4, tg.MatrizId);
            Assert.Equal("5º Ano", tg.Descricao);
            Assert.Equal(1, tg.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, tg.Status);
            Assert.InRange(tg.CriadoEm, antes, depois);
            Assert.InRange(tg.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var tg = new TipoGrade(11, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(11, tg.Id);
            Assert.InRange(tg.AlteradoEm, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var tg = new TipoGrade(1, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            Assert.False(tg.PossuiAlteracao(4, "5º Ano", 1, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_MatrizId_Diferente()
        {
            var tg = new TipoGrade(1, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            Assert.True(tg.PossuiAlteracao(99, "5º Ano", 1, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var tg = new TipoGrade(1, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            Assert.True(tg.PossuiAlteracao(4, "6º Ano", 1, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Ordem_Diferente()
        {
            var tg = new TipoGrade(1, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            Assert.True(tg.PossuiAlteracao(4, "5º Ano", 2, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var tg = new TipoGrade(1, 8, 4, "5º Ano", 1, StatusGeral.Ativo);
            Assert.True(tg.PossuiAlteracao(4, "5º Ano", 1, StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_Ordem_Zero()
        {
            var tg = new TipoGrade(null, 8, 4, "5º Ano", 0, StatusGeral.Ativo);
            Assert.Equal(0, tg.Ordem);
        }
    }
}