using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class FilialRequest
    {
        //[Required(ErrorMessage = "O nome da filial é obrigatório.")]
        //[MaxLength(60, ErrorMessage = "O nome deve ter no máximo 60 caracteres.")]
        //[RegularExpression(@"^[A-Za-zÀ-ÿ0-9\s\-]+$",
        //    ErrorMessage = "O nome da filial deve conter apenas letras, números, espaços e traços.")]
        //public string Nome { get; set; }

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
