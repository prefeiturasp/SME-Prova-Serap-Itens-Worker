
namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class AreaConhecimento : EntidadeBase
    {
        public AreaConhecimento() { }
        public AreaConhecimento(string descricao)
        {
            Descricao = descricao;
        }
        public long AreaConhecimentoLegadoId { get; set; }
        public string Descricao { get; set; }
    }
}
