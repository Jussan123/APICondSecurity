using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Rfid
{
    public int IdRfid { get; set; }

    public int Numero { get; set; }

    public char Situacao { get; set; }

    public int? IdCondominio { get; set; }
}
