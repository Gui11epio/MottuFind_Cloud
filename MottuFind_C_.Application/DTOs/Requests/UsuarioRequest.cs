using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Application.DTOs.Requests;

public class UsuarioRequest
{
    public Setores Setores { get; set; }
    public string NomeUsuario { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}