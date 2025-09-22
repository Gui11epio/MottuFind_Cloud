using System.ComponentModel.DataAnnotations;
using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Application.DTOs.Requests;

public class UsuarioRequest
{
    [Required(ErrorMessage = "O setor é obrigatório")]
    public Setores Setores { get; set; }

    [Required(ErrorMessage = "O nome do usuário é obrigatório")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O nome do usuário deve ter entre 3 e 100 caracteres")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ\s'-]+$",
        ErrorMessage = "O nome do usuário contém caracteres inválidos")]
    public string NomeUsuario { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(50, MinimumLength = 8,
        ErrorMessage = "A senha deve ter no mínimo 8 e no máximo 50 caracteres")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial (@$!%*?&)")]
    public string Senha { get; set; }
}