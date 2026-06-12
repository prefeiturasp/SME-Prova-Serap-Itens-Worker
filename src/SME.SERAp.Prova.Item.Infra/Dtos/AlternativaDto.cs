namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class AlternativaDto
    {
        public long? Id { get; set; }
        public string Descricao { get; set; }
        public string Justificativa { get; set; }
        public string Numeracao { get; set; }
        public bool Correta { get; set; }
        public int Ordem { get; set; }
    }
}