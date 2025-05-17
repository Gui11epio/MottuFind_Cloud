namespace Sprint1_C_.Application.DTOs.Response
{
    public class PagedResult<T>
    {
        public int Numeropag { get; set; }
        public int Tamnhopag { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }

}
