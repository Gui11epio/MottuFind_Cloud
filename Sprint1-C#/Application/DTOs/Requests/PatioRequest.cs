using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class PatioRequest
    {

        public string Nome { get; set; }
        public string Largura { get; set; }
        public string Comprimento { get; set; }
        public int FilialId { get; set; }
    }
}
