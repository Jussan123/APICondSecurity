using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Condominio
{
    public int IdCondominio { get; set; }

    public string Nome { get; set; } = null!;

    public char Situacao { get; set; }

    public int? IdEndereco { get; set; }
}
