using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeituraRfidRequest
    {
        

        public DateTime DataHora { get; set; }

        public int LeitorId { get; set; } 
       
        public int TagId { get; set; } 
        
    }
}
