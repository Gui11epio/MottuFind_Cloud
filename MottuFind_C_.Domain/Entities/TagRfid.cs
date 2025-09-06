namespace Sprint1_C_.Domain.Entities
{
    public class TagRfid
    {
        public int Id { get; set; }
        public string CodigoIdentificacao { get; set; } 
        // Relação com a tabela de Moto (One to one)
        public string MotoPlaca { get; set; }
        public Moto Moto { get; set; }

    }
}
