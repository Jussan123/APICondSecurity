using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Permissao
{
    public int IdPermissao { get; set; }

    public char Situacao { get; set; }

    public int? IdNotificacao { get; set; }
}
