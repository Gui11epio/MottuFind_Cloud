using System.Runtime.InteropServices;

namespace Sprint1_C_.Domain.Entities
{
    public class LeitorRfid
    {
        public int Id { get; set; }

        public string Localizacao { get; set; } // Exemplo: "Entrada principal", "Área B"
        public string IpDispositivo { get; set; } // IP do leitor IoT

        // Relacionamento com a tabela Patio(Many to one)
        public Patio Patio { get; set; }

        // Relacionamento com a tabela LeituraRfid(One to many)
        public List<LeituraRfid> Leituras { get; set; }
    }
}
