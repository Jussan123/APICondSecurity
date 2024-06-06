using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Cidade
{
    public int IdCidade { get; set; }

    public string Nome { get; set; } = null!;

    public int CidadeIbge { get; set; }
}
