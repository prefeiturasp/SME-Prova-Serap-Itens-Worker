using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class DificuldadeTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var dificuldade = new Dificuldade();
            Assert.Equal(0, dificuldade.Id);
            Assert.Null(dificuldade.Descricao);
            Assert.Equal(0, dificuldade.Ordem);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var dificuldade = new Dificuldade(null, 1, "Fácil", 1, StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, dificuldade.Id);
            Assert.Equal(1, dificuldade.LegadoId);
            Assert.Equal("Fácil", dificuldade.Descricao);
            Assert.Equal(1, dificuldade.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, dificuldade.Status);
            Assert.InRange(dificuldade.CriadoEm, antes, depois);
            Assert.InRange(dificuldade.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var dificuldade = new Dificuldade(3, 1, "Fácil", 1, StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(3, dificuldade.Id);
            Assert.InRange(dificuldade.AlteradoEm, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var dificuldade = new Dificuldade(1, 1, "Fácil", 1, StatusGeral.Ativo);
            Assert.False(dificuldade.PossuiAlteracao("Fácil", 1, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var dificuldade = new Dificuldade(1, 1, "Fácil", 1, StatusGeral.Ativo);
            Assert.True(dificuldade.PossuiAlteracao("Difícil", 1, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Ordem_Diferente()
        {
            var dificuldade = new Dificuldade(1, 1, "Fácil", 1, StatusGeral.Ativo);
            Assert.True(dificuldade.PossuiAlteracao("Fácil", 2, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var dificuldade = new Dificuldade(1, 1, "Fácil", 1, StatusGeral.Ativo);
            Assert.True(dificuldade.PossuiAlteracao("Fácil", 1, StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_Ordem_Zero()
        {
            var dificuldade = new Dificuldade(null, 1, "Fácil", 0, StatusGeral.Ativo);
            Assert.Equal(0, dificuldade.Ordem);
        }
    }
}