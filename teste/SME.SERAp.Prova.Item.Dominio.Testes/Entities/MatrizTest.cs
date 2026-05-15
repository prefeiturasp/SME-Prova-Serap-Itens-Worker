using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class MatrizTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var matriz = new Matriz();
            Assert.Equal(0, matriz.Id);
            Assert.Null(matriz.Descricao);
            Assert.Null(matriz.Modelo);
        }

        [Fact]
        public void Deve_Criar_Nova_Com_Id_Nulo()
        {
            var antes = DateTime.Now;
            var matriz = new Matriz(null, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(0, matriz.Id);
            Assert.Equal(5, matriz.LegadoId);
            Assert.Equal(2, matriz.DisciplinaId);
            Assert.Equal("Matriz de Referência", matriz.Descricao);
            Assert.Equal("Modelo A", matriz.Modelo);
            Assert.Equal((int)StatusGeral.Ativo, matriz.Status);
            Assert.InRange(matriz.CriadoEm, antes, depois);
            Assert.InRange(matriz.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Criar_Com_Id_Existente()
        {
            var antes = DateTime.Now;
            var matriz = new Matriz(9, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            var depois = DateTime.Now;

            Assert.Equal(9, matriz.Id);
            Assert.InRange(matriz.AlteradoEm, antes, depois);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var matriz = new Matriz(1, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            Assert.False(matriz.PossuiAlteracao("Matriz de Referência", "Modelo A", 2, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var matriz = new Matriz(1, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            Assert.True(matriz.PossuiAlteracao("Nova Matriz", "Modelo A", 2, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Modelo_Diferente()
        {
            var matriz = new Matriz(1, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            Assert.True(matriz.PossuiAlteracao("Matriz de Referência", "Modelo B", 2, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_DisciplinaId_Diferente()
        {
            var matriz = new Matriz(1, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            Assert.True(matriz.PossuiAlteracao("Matriz de Referência", "Modelo A", 99, StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var matriz = new Matriz(1, 5, 2, "Matriz de Referência", "Modelo A", StatusGeral.Ativo);
            Assert.True(matriz.PossuiAlteracao("Matriz de Referência", "Modelo A", 2, StatusGeral.Inativo));
        }

        [Fact]
        public void Deve_Criar_Com_Modelo_Nulo()
        {
            var matriz = new Matriz(null, 5, 2, "Matriz de Referência", null, StatusGeral.Ativo);
            Assert.Null(matriz.Modelo);
        }
    }
}