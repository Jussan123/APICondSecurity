using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Endereco
{
    public int IdEndereco { get; set; }

    public string Rua { get; set; } = null!;

    public int? Numero { get; set; }

    public string Bairro { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string? Complemento { get; set; }

    public int? IdUf { get; set; }
}
