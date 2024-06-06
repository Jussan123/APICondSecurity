using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class VeiculoUsuario
{
    public int IdVeiculoUsuario { get; set; }

    public string Placa { get; set; } = null!;

    public int IdUsuario { get; set; }

    public int? IdVeiculo { get; set; }

    public int IdRfid { get; set; }
}
