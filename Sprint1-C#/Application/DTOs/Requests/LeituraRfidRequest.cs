using Sprint1_C_.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeituraRfidRequest
    {

        [Required(ErrorMessage = "A data e hora são obrigatórias")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O ID do leitor é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do leitor deve ser positivo")]
        public int LeitorId { get; set; }

        [Required(ErrorMessage = "O ID da tag é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID da tag deve ser positivo")]
        public int TagId { get; set; } 
        
    }
}
