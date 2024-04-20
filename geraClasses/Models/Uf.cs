using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Uf
{
    public int IdUf { get; set; }

    public string Nome { get; set; } = null!;

    public string Sigla { get; set; } = null!;

    public int? IdCidade { get; set; }
}
