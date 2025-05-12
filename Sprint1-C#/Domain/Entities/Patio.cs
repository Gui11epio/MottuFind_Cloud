namespace Sprint1_C_.Domain.Entities
{
    public class Patio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Largura { get; set; }
        public string Comprimento { get; set; }

        // Relacionamento com a tabela de Filiais(Many to one)
        public Filial Filial { get; set; }

        // Relacionamento com a tabela de Leitores RFID(One to many)
        public List<LeitorRfid> Leitores { get; set; } = new List<LeitorRfid>();

        // Relacionamento com a tabela de Motos(One to many)
        public List<Moto> Motos { get; set; } = new List<Moto>();
    }
}
