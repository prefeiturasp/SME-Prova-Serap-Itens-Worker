using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("grupo")]
    public class Grupo : EntidadeBase
    {
        public Grupo() { }

        public Grupo(Guid legadoId, string nome, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            LegadoId = legadoId;
            Nome = nome;
            PermiteConsultar = permiteConsultar;
            PermiteInserir = permiteInserir;
            PermiteAlterar = permiteAlterar;
            PermiteExcluir = permiteExcluir;
            CriadoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Alterar(string nome, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            Nome = nome;
            PermiteConsultar = permiteConsultar;
            PermiteInserir = permiteInserir;
            PermiteAlterar = permiteAlterar;
            PermiteExcluir = permiteExcluir;
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Inativar()
        {
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Inativo;
        }

        [Column("legado_id")]
        public Guid LegadoId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("permite_consultar")]
        public bool PermiteConsultar { get; set; }

        [Column("permite_inserir")]
        public bool PermiteInserir { get; set; }

        [Column("permite_alterar")]
        public bool PermiteAlterar { get; set; }

        [Column("permite_excluir")]
        public bool PermiteExcluir { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }

        [Column("status")]
        public StatusGeral Status { get; set; }

        public bool PossuiAlteracao(string nome, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
            => Nome != nome || PermiteConsultar != permiteConsultar || PermiteInserir != permiteInserir
            || PermiteAlterar != permiteAlterar || PermiteExcluir != permiteExcluir;
    }
}