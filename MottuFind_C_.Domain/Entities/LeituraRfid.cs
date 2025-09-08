using System.Runtime.InteropServices;

namespace Sprint1_C_.Domain.Entities
{
    public class LeituraRfid
    {
        public int Id { get; set; }

        public DateTime DataHora { get; set; }

        // Relação com a tabela de Leitor RFID(Many to one)
        public int LeitorId { get; set; }
        public LeitorRfid Leitor { get; set; }

        // Relação com a tabela de TagRfid(Many to one)
        public int TagRfidId { get; set; }
        public TagRfid TagRfid { get; set; }

    }
}
