using Sprint1_C_.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeituraRfidRequest
    {

        [Required(ErrorMessage = "A data e hora são obrigatórias")]
        [DataHoraPassadoOuPresente(ErrorMessage = "A data e hora da leitura não pode estar no futuro")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O ID do leitor é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do leitor deve ser positivo")]
        public int LeitorId { get; set; }

        [Required(ErrorMessage = "O ID da Tag RFID é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID da Tag RFID deve ser positivo")]
        public int TagRfidId { get; set; }



    }


    public class DataHoraPassadoOuPresenteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dataHora)
            {
                if (dataHora > DateTime.UtcNow.AddMinutes(1)) // tolerância de 1 min
                {
                    return new ValidationResult(ErrorMessage ?? "A data/hora não pode ser no futuro");
                }
            }

            return ValidationResult.Success;
        }
    }
}
