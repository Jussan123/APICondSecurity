using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Portao
{
    public int IdPortao { get; set; }

    public string Nome { get; set; } = null!;

    public int? IdCondominio { get; set; }
}
