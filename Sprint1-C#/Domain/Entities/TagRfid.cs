using System.Runtime.InteropServices;

namespace Sprint1_C_.Domain.Entities
{
    public class TagRfid
    {
        public int Id { get; set; }

        public string CodigoIdentificacao { get; set; }

        public bool Ativa {  get; set; }
        // Relação com a tabela de Motos(One to one)
        public Moto Moto { get; set; }
    }
}
