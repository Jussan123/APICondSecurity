using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class Notificacao
{
    public int IdNotificacao { get; set; }

    public string? Mensagem { get; set; }

    public TimeOnly DataHora { get; set; }

    public char? Tipo { get; set; }

    public string? Imagem { get; set; }

    public char? Situacao { get; set; }

    public int? IdUsuario { get; set; }
}
