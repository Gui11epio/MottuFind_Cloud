using Sprint1_C_.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class MotoRequest
    {
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "Placa inválida. Use o formato ABC1D23.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public MotoModelo Modelo { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "A marca deve conter apenas letras e espaços.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O status da moto é obrigatório.")]
        public MotoStatus Status { get; set; }

        [Required(ErrorMessage = "O Id do pátio é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do pátio deve ser positivo.")]
        public int PatioId { get; set; }

        [Required(ErrorMessage = "O Id da tag é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID da tag deve ser positivo.")]
        public int TagId { get; set; }
    }
}
