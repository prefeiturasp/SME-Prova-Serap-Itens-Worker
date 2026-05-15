namespace SME.SERAp.Prova.Item.Dominio.Testes.Entities
{
    public class HabilidadeTest
    {
        [Fact]
        public void Deve_Criar_Com_Construtor_Padrao()
        {
            var habilidade = new Habilidade();
            Assert.Equal(0, habilidade.Id);
            Assert.Null(habilidade.Codigo);
            Assert.Null(habilidade.Descricao);
        }

        [Fact]
        public void Deve_Criar_Com_Construtor_Parametros()
        {
            var antes = DateTime.Now;
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            var depois = DateTime.Now;

            Assert.Equal(10, habilidade.LegadoId);
            Assert.Equal(3, habilidade.CompetenciaId);
            Assert.Equal("H01", habilidade.Codigo);
            Assert.Equal("Identificar informações", habilidade.Descricao);
            Assert.Equal(StatusGeral.Ativo, habilidade.Status);
            Assert.InRange(habilidade.CriadoEm, antes, depois);
            Assert.InRange(habilidade.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Alterar_Habilidade()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            var antes = DateTime.Now;
            habilidade.Alterar(5, "H02", "Interpretar gráficos");
            var depois = DateTime.Now;

            Assert.Equal(5, habilidade.CompetenciaId);
            Assert.Equal("H02", habilidade.Codigo);
            Assert.Equal("Interpretar gráficos", habilidade.Descricao);
            Assert.InRange(habilidade.AlteradoEm, antes, depois);
        }

        [Fact]
        public void Deve_Inativar_Habilidade()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            habilidade.Inativar();
            Assert.Equal(StatusGeral.Inativo, habilidade.Status);
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_False_Quando_Igual()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.False(habilidade.PossuiAlteracao(3, "H01", "Identificar informações", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_CompetenciaId_Diferente()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.True(habilidade.PossuiAlteracao(99, "H01", "Identificar informações", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Codigo_Diferente()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.True(habilidade.PossuiAlteracao(3, "H99", "Identificar informações", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Descricao_Diferente()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.True(habilidade.PossuiAlteracao(3, "H01", "Nova descrição", StatusGeral.Ativo));
        }

        [Fact]
        public void PossuiAlteracao_Deve_Retornar_True_Quando_Status_Diferente()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.True(habilidade.PossuiAlteracao(3, "H01", "Identificar informações", StatusGeral.Inativo));
        }

        [Fact]
        public void Status_Deve_Ser_Ativo_Apos_Criacao()
        {
            var habilidade = new Habilidade(10, 3, "H01", "Identificar informações");
            Assert.Equal(StatusGeral.Ativo, habilidade.Status);
        }
    }
}