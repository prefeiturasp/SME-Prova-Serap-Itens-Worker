namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class AreaConhecimentoTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var area = new AreaConhecimento();
            Assert.Equal(0, area.Id);
            Assert.Equal(0, area.LegadoId);
            Assert.Null(area.Descricao);
            Assert.Equal(0, area.Status);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var area = new AreaConhecimento(null, 10, "Matemática", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, area.Id);
            Assert.Equal(10, area.LegadoId);
            Assert.Equal("Matemática", area.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, area.Status);
            Assert.InRange(area.CriadoEm, antes, depois);
            Assert.InRange(area.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var area = new AreaConhecimento(5, 10, "Matemática", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(5, area.Id);
            Assert.Equal(10, area.LegadoId);
            Assert.Equal("Matemática", area.Descricao);
            Assert.InRange(area.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Novo_Com_Status_Do_Parametro()
        {
            long? id = null;
            var area = new AreaConhecimento(id, 10, "Matemática", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, area.Status);
        }

        [Fact]
        public void Deve_Criar_Com_Descricao_Nula()
        {
            var area = new AreaConhecimento(null, 10, null, StatusGeral.Ativo);
            Assert.Null(area.Descricao);
        }

        [Fact]
        public void Deve_Criar_Com_LegadoId_Maximo()
        {
            var area = new AreaConhecimento(null, long.MaxValue, "Matemática", StatusGeral.Ativo);
            Assert.Equal(long.MaxValue, area.LegadoId);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var area = new AreaConhecimento(1, 10, "Matemática", StatusGeral.Ativo);
            Assert.False(area.PossuiAlteracao("Matemática", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var area = new AreaConhecimento(1, 10, "Matemática", StatusGeral.Ativo);
            Assert.True(area.PossuiAlteracao("Ciências", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var area = new AreaConhecimento(1, 10, "Matemática", StatusGeral.Ativo);
            Assert.True(area.PossuiAlteracao("Matemática", StatusGeral.Inativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Ambos_Diferentes()
        {
            var area = new AreaConhecimento(1, 10, "Matemática", StatusGeral.Ativo);
            Assert.True(area.PossuiAlteracao("Ciências", StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Permitir_Alterar_Propriedades_Apos_Criacao()
        {
            var area = new AreaConhecimento();
            area.Descricao = "Nova Descrição";
            area.LegadoId = 99;
            area.Status = (int)StatusGeral.Ativo;

            Assert.Equal("Nova Descrição", area.Descricao);
            Assert.Equal(99, area.LegadoId);
            Assert.Equal((int)StatusGeral.Ativo, area.Status);
        }
    }
}