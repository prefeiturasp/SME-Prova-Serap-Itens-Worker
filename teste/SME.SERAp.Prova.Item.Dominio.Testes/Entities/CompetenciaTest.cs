namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class CompetenciaTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var competencia = new Competencia();
            Assert.Equal(0, competencia.Id);
            Assert.Null(competencia.Codigo);
            Assert.Null(competencia.Descricao);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var competencia = new Competencia(null, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, competencia.Id);
            Assert.Equal("C01", competencia.Codigo);
            Assert.Equal(5, competencia.LegadoId);
            Assert.Equal(3, competencia.MatrizId);
            Assert.Equal("Interpretar textos", competencia.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, competencia.Status);
            Assert.InRange(competencia.CriadoEm, antes, depois);
            Assert.InRange(competencia.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var competencia = new Competencia(8, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(8, competencia.Id);
            Assert.InRange(competencia.AlteradoEm, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var competencia = new Competencia(1, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            Assert.False(competencia.PossuiAlteracao("C01", 3, "Interpretar textos", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Codigo_Diferente()
        {
            var competencia = new Competencia(1, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            Assert.True(competencia.PossuiAlteracao("C02", 3, "Interpretar textos", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_MatrizId_Diferente()
        {
            var competencia = new Competencia(1, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            Assert.True(competencia.PossuiAlteracao("C01", 99, "Interpretar textos", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var competencia = new Competencia(1, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            Assert.True(competencia.PossuiAlteracao("C01", 3, "Produzir textos", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var competencia = new Competencia(1, "C01", 5, 3, "Interpretar textos", (int)StatusGeral.Ativo);
            Assert.True(competencia.PossuiAlteracao("C01", 3, "Interpretar textos", StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_Descricao_Nula()
        {
            var competencia = new Competencia(null, "C01", 5, 3, null, (int)StatusGeral.Ativo);
            Assert.Null(competencia.Descricao);
        }
    }
}