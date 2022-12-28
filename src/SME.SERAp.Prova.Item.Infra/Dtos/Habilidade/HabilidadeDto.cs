namespace SME.SERAp.Prova.Item.Infra.Dtos.Habilidade
{
    public class HabilidadeDto
    {
        public HabilidadeDto(int id, string codigo, string descricao, bool ultimoNivel)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            UltimoNivel = ultimoNivel;
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool UltimoNivel { get; set; }
    }
}
