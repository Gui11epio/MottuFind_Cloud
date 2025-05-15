
using System.ComponentModel.DataAnnotations;
namespace Sprint1_C_.Application.DTOs.Requests
{
    public class TagRfidRequest
    {
        [Required(ErrorMessage = "O código de identificação é obrigatório")]
        public string CodigoIdentificacao { get; set; }

        [Required(ErrorMessage = "O status de ativação é obrigatório")]
        public bool Ativa { get; set; }
        
    }
}
