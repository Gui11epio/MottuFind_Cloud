namespace Sprint1_C_.Domain.Entities
{
    public class Filial
    {
        public int Id { get; set; }
        
        public string Cidade { get; set; }
        public string Pais { get; set; }
        
        
        public ICollection<Patio> Patios { get; set; } = new List<Patio>();
    }
}
