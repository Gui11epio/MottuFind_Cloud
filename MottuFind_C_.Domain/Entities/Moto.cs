using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Sprint1_C_.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sprint1_C_.Domain.Entities
{
    public class Moto
    {
        [Key]
        public string Placa { get; set; }
        public MotoModelo Modelo { get; set; }
        public string Marca { get; set; }
        public MotoStatus Status { get; set; }

        // Relacionamento com a tabela de Patio(Many to one)
        public int PatioId { get; set; }
        public Patio Patio { get; set; }
        // Relacionamento com a tabela de TagRfid(One to one)
        public TagRfid TagRfid { get; set; }

      
    }
}
