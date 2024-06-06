using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Camera
{
    public int IdCamera { get; set; }

    public string? Posicao { get; set; }

    public string? Tipo { get; set; }

    public string IpCamera { get; set; } = null!;

    public int IdCondominio { get; set; }
}
