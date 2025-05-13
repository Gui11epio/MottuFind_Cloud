namespace Sprint1_C_.Domain.Entities
{
    public class Filial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        
        // Relacionamento com a tabela de Patios(One to Many)
        public ICollection<Patio> Patios { get; set; } = new List<Patio>();
    }
}
