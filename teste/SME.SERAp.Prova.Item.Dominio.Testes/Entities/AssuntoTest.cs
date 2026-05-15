namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class AssuntoTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var assunto = new Assunto();
            Assert.Equal(0, assunto.Id);
            Assert.Null(assunto.Descricao);
            Assert.Equal(0, assunto.Status);
        }

        [Fact]
        public void Deve_Criar_Novo_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var assunto = new Assunto(null, 10, 2, "Frações", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, assunto.Id);
            Assert.Equal(10, assunto.LegadoId);
            Assert.Equal(2, assunto.DisciplinaId);
            Assert.Equal("Frações", assunto.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, assunto.Status);
            Assert.InRange(assunto.CriadoEm, antes, depois);
            Assert.InRange(assunto.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var assunto = new Assunto(7, 10, 2, "Frações", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(7, assunto.Id);
            Assert.Equal(10, assunto.LegadoId);
            Assert.Equal(2, assunto.DisciplinaId);
            Assert.Equal("Frações", assunto.Descricao);
            Assert.InRange(assunto.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Descricao_Nula()
        {
            var assunto = new Assunto(null, 10, 2, null, StatusGeral.Ativo);
            Assert.Null(assunto.Descricao);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var assunto = new Assunto(1, 10, 2, "Frações", StatusGeral.Ativo);
            Assert.False(assunto.PossuiAlteracao("Frações", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var assunto = new Assunto(1, 10, 2, "Frações", StatusGeral.Ativo);
            Assert.True(assunto.PossuiAlteracao("Decimais", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var assunto = new Assunto(1, 10, 2, "Frações", StatusGeral.Ativo);
            Assert.True(assunto.PossuiAlteracao("Frações", StatusGeral.Inativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Ambos_Diferentes()
        {
            var assunto = new Assunto(1, 10, 2, "Frações", StatusGeral.Ativo);
            Assert.True(assunto.PossuiAlteracao("Decimais", StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_LegadoId_Maximo()
        {
            var assunto = new Assunto(null, long.MaxValue, 1, "Frações", StatusGeral.Ativo);
            Assert.Equal(long.MaxValue, assunto.LegadoId);
        }
    }
}