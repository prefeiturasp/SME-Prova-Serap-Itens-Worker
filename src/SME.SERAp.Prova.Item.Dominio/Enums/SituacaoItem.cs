using System.ComponentModel;

namespace SME.SERAp.Prova.Item.Dominio.Enums
{
    public enum SituacaoItem
    {
        [Description("Inativo")]
        Inativo = 0,
        [Description("Ativo")]
        Ativo = 1,
        [Description("Pendente")]
        Pendente = 2,
        [Description("Rascunho")]
        Rascunho = 3
    }
}