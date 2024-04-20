using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class TipoUsuario
{
    public int IdTipoUsuario { get; set; }

    public string Tipo { get; set; } = null!;
}
