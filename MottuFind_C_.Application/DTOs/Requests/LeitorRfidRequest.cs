using Sprint1_C_.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeitorRfidRequest
    {
        [Required(ErrorMessage = "A localização é obrigatória")]
        [StringLength(100, MinimumLength = 3,
        ErrorMessage = "A localização deve ter entre 3 e 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ0-9\s\-\.,]+$",
        ErrorMessage = "A localização contém caracteres inválidos")]
        public string Localizacao { get; set; }

        [Required(ErrorMessage = "O IP do dispositivo é obrigatório")]
        [RegularExpression(
        @"^(?:(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(?:\.|$)){4}$",
        ErrorMessage = "O IP do dispositivo deve estar no formato IPv4 válido (ex: 192.168.0.1)")]
        public string IpDispositivo { get; set; }

        [Required(ErrorMessage = "O ID do pátio é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do pátio deve ser positivo")]
        public int PatioId { get; set; }
        
    }
}
