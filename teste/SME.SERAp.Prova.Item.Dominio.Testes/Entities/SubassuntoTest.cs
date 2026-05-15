namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class SubassuntoTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var sub = new Subassunto();
            Assert.Equal(0, sub.Id);
            Assert.Null(sub.Descricao);
            Assert.Equal(0, sub.AssuntoId);
        }

        [Fact]
        public void Deve_Criar_Novo_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var sub = new Subassunto(null, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, sub.Id);
            Assert.Equal(5, sub.LegadoId);
            Assert.Equal(3, sub.AssuntoId);
            Assert.Equal("Números Inteiros", sub.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, sub.Status);
            Assert.InRange(sub.CriadoEm, antes, depois);
            Assert.InRange(sub.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var sub = new Subassunto(6, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(6, sub.Id);
            Assert.InRange(sub.AlteradoEm, antes, depois);
        }

        [Fact]
        public void AssuntoId_Deve_Ter_Setter_Privado()
        {
            var sub = new Subassunto(null, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            Assert.Equal(3, sub.AssuntoId);

            var prop = typeof(Subassunto).GetProperty("AssuntoId");
            Assert.NotNull(prop?.SetMethod);
            Assert.True(prop!.SetMethod!.IsPrivate);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var sub = new Subassunto(1, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            Assert.False(sub.PossuiAlteracao(3, "Números Inteiros", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_AssuntoId_Diferente()
        {
            var sub = new Subassunto(1, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            Assert.True(sub.PossuiAlteracao(99, "Números Inteiros", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var sub = new Subassunto(1, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            Assert.True(sub.PossuiAlteracao(3, "Números Racionais", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var sub = new Subassunto(1, 5, 3, "Números Inteiros", StatusGeral.Ativo);
            Assert.True(sub.PossuiAlteracao(3, "Números Inteiros", StatusGeral.Inativo));
        }
    }
}