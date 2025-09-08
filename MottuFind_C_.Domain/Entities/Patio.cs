namespace Sprint1_C_.Domain.Entities
{
    public class Patio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
     
        // Relacionamento com a tabela de Filiais(Many to one)
        public int FilialId { get; set; }
        public Filial Filial { get; set; }

        // Relacionamento com a tabela de Leitores RFID(One to many)
        public ICollection<LeitorRfid> Leitores { get; set; } = new List<LeitorRfid>();

        // Relacionamento com a tabela de Motos(One to many)
        public ICollection<Moto> Motos { get; set; } = new List<Moto>();
    }
}
