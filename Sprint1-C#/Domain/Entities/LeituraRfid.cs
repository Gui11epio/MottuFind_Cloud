using System.Runtime.InteropServices;

namespace Sprint1_C_.Domain.Entities
{
    public class LeituraRfid
    {
        public int Id { get; set; }

        public DateTime DataHora { get; set; } // Momento da leitura

        // Relação com a tabela de Leitor RFID(Many to one)
        public LeitorRfid Leitor { get; set; }

        // Relação com a tabela de TagRfis(Many to one)
        public TagRfid Tag { get; set; }
    }
}
