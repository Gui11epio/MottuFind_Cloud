using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Domain.Entities
{
    public class Moto
    {
        public string Placa { get; set; }
        public MotoModelo Modelo { get; set; }
        public string Marca { get; set; }
        public MotoStatus Status { get; set; }

        // Relacionamento com a tabela de Patio(Many to one)
        public Patio Patio { get; set; }
        // Relacionamento com a tabela de TagRfid(One to one)
        public TagRfid Tag { get; set; }
    }
}
