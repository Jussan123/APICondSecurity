﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Models;

[Table("tipo_usuario")]
public partial class TipoUsuario
{
    [Key]
    [Column("id_tipo_usuario")]
    public int IdTipoUsuario { get; set; }

    [Required]
    [Column("tipo")]
    [StringLength(15)]
    public string Tipo { get; set; }

    [InverseProperty("IdTipoUsuarioNavigation")]
    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}