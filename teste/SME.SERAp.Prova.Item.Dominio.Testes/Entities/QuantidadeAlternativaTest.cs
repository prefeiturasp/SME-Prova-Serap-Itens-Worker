namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class QuantidadeAlternativaTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var qa = new QuantidadeAlternativa();
            Assert.Equal(0, qa.Id);
            Assert.Null(qa.Descricao);
            Assert.Equal(0, qa.QtdeAlternativa);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var qa = new QuantidadeAlternativa(null, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, qa.Id);
            Assert.Equal(10, qa.LegadoId);
            Assert.True(qa.EhPadrao);
            Assert.Equal(4, qa.QtdeAlternativa);
            Assert.Equal("4 alternativas", qa.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, qa.Status);
            Assert.InRange(qa.CriadoEm, antes, depois);
            Assert.InRange(qa.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var qa = new QuantidadeAlternativa(2, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(2, qa.Id);
            Assert.InRange(qa.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_EhPadrao_False()
        {
            var qa = new QuantidadeAlternativa(null, 10, false, 5, "5 alternativas", (int)StatusGeral.Ativo);
            Assert.False(qa.EhPadrao);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var qa = new QuantidadeAlternativa(1, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            Assert.False(qa.PossuiAlteracao(true, 4, "4 alternativas", (int)StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_EhPadrao_Diferente()
        {
            var qa = new QuantidadeAlternativa(1, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            Assert.True(qa.PossuiAlteracao(false, 4, "4 alternativas", (int)StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_QtdeAlternativa_Diferente()
        {
            var qa = new QuantidadeAlternativa(1, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            Assert.True(qa.PossuiAlteracao(true, 5, "4 alternativas", (int)StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var qa = new QuantidadeAlternativa(1, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            Assert.True(qa.PossuiAlteracao(true, 4, "outra descrição", (int)StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var qa = new QuantidadeAlternativa(1, 10, true, 4, "4 alternativas", (int)StatusGeral.Ativo);
            Assert.True(qa.PossuiAlteracao(true, 4, "4 alternativas", (int)StatusGeral.Inativo));
        }
    }
}