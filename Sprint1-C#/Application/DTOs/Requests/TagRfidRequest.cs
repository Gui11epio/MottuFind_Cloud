namespace Sprint1_C_.Application.DTOs.Requests
{
    public class TagRfidRequest
    {
        public int Id { get; set; }
        public string CodigoIdentificacao { get; set; }
        public bool Ativa { get; set; }
        public string MotoPlaca { get; set; }
    }
}
