using System;
using System.Collections.Generic;

namespace APICondSecurity.Domain.Models;

public partial class VeiculoTerceiro
{
    public int IdVeiculoTerceiro { get; set; }

    public string Placa { get; set; } = null!;

    public int? IdNotificacao { get; set; }

    public int? IdPermissao { get; set; }

    public int? IdVeiculo { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Notificacao? IdNotificacaoNavigation { get; set; }

    public virtual Permissao? IdPermissaoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
