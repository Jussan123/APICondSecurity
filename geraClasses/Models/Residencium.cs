using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Residencium
{
    public int IdResidencia { get; set; }

    public int Numero { get; set; }

    public string? Bloco { get; set; }

    public string? Quadra { get; set; }

    public string? Rua { get; set; }

    public int? IdCondominio { get; set; }
}
