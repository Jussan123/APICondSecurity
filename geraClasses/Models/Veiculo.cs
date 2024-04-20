using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Veiculo
{
    public int IdVeiculo { get; set; }

    public string Placa { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Cor { get; set; } = null!;

    public int Ano { get; set; }

    public int IdCondominio { get; set; }

    public string? Situacao { get; set; }
}
