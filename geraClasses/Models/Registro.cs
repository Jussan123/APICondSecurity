using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Registro
{
    public int IdRegistros { get; set; }

    public DateTime? DataHoraEntrada { get; set; }

    public DateTime? DataHoraSaida { get; set; }

    public string Placa { get; set; } = null!;

    public int? IdVeiculoUsuario { get; set; }

    public int? IdPortao { get; set; }

    public int? IdVeiculoTerceiro { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdVeiculo { get; set; }
}
