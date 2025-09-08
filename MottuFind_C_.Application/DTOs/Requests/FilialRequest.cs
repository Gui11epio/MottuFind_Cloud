using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class FilialRequest
    {
        
        [Required(ErrorMessage = "A cidade da filial é obrigatória")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$",
        ErrorMessage = "A cidade deve conter apenas letras e espaços.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O país da filial é obrigatório")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$",
        ErrorMessage = "O país deve conter apenas letras e espaços.")]
        public string Pais { get; set; }
    }
}
