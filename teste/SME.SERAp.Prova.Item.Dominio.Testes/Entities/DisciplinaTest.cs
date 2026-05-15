using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class DisciplinaTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var disciplina = new Disciplina();
            Assert.Equal(0, disciplina.Id);
            Assert.Null(disciplina.Descricao);
            Assert.Null(disciplina.NivelEnsino);
            Assert.Equal(0, disciplina.AreaConhecimentoId);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var disciplina = new Disciplina(null, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, disciplina.Id);
            Assert.Equal(20, disciplina.LegadoId);
            Assert.Equal(3, disciplina.AreaConhecimentoId);
            Assert.Equal("Matemática", disciplina.Descricao);
            Assert.Equal("EF", disciplina.NivelEnsino);
            Assert.Equal((int)StatusGeral.Ativo, disciplina.Status);
            Assert.InRange(disciplina.CriadoEm, antes, depois);
            Assert.InRange(disciplina.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var disciplina = new Disciplina(4, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(4, disciplina.Id);
            Assert.InRange(disciplina.AlteradoEm, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var disciplina = new Disciplina(1, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            Assert.False(disciplina.PossuiAlteracao("Matemática", "EF", 3, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var disciplina = new Disciplina(1, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            Assert.True(disciplina.PossuiAlteracao("Português", "EF", 3, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_NivelEnsino_Diferente()
        {
            var disciplina = new Disciplina(1, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            Assert.True(disciplina.PossuiAlteracao("Matemática", "EM", 3, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_AreaConhecimentoId_Diferente()
        {
            var disciplina = new Disciplina(1, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            Assert.True(disciplina.PossuiAlteracao("Matemática", "EF", 99, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var disciplina = new Disciplina(1, 20, 3, "Matemática", "EF", StatusGeral.Ativo);
            Assert.True(disciplina.PossuiAlteracao("Matemática", "EF", 3, StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_NivelEnsino_Nulo()
        {
            var disciplina = new Disciplina(null, 20, 3, "Matemática", null, StatusGeral.Ativo);
            Assert.Null(disciplina.NivelEnsino);
        }
    }
}