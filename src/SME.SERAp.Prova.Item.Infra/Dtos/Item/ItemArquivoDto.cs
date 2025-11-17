using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Item
{
    public class ItemArquivoDto
    {
        public long Id { get; set; }          // ID da tabela 'item_audio' ou 'item_video'
        public long ArquivoId { get; set; }   // ID da tabela 'arquivo'
        public long LegadoId { get; set; }    // O ID que o sistema Legado conhece
        public string NomeArquivo { get; set; }
        public string ContentType { get; set; }
    }
}