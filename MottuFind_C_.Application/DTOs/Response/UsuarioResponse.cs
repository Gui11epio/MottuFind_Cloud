using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Application.DTOs.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public Setores Setores { get; set; }
    public string NomeUsuario { get; set; }
    public string Email { get; set; }
}