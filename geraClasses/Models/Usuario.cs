using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public int Telefone { get; set; }

    public int? IdResidencia { get; set; }

    public int? IdCondominio { get; set; }

    public int? IdTipoUsuario { get; set; }

    public string? Situacao { get; set; }
}
