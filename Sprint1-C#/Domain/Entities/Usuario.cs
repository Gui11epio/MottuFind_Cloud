using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public Setores Setor { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
