namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class EntidadeBaseTest
    {
        [Fact]
        public void Deve_Ter_Id_Zero_Por_Padrao()
        {
            var area = new AreaConhecimento();
            Assert.Equal(0, area.Id);
        }

        [Fact]
        public void Deve_Permitir_Definir_Id()
        {
            var area = new AreaConhecimento();
            area.Id = 42;
            Assert.Equal(42, area.Id);
        }

        [Fact]
        public void Deve_Permitir_Id_Maximo()
        {
            var area = new AreaConhecimento();
            area.Id = long.MaxValue;
            Assert.Equal(long.MaxValue, area.Id);
        }

        [Fact]
        public void Deve_Herdar_Id_Corretamente()
        {
            var area = new AreaConhecimento(10, 1, "Matemática", StatusGeral.Ativo);
            Assert.Equal(10, area.Id);
        }
    }
}