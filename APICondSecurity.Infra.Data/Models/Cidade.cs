﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Models;

[Table("cidade")]
public partial class Cidade
{
    [Key]
    [Column("id_cidade")]
    public int IdCidade { get; set; }

    [Required]
    [Column("nome")]
    [StringLength(50)]
    public string Nome { get; set; }

    [Column("cidade_ibge")]
    public int CidadeIbge { get; set; }

    [InverseProperty("IdCidadeNavigation")]
    public virtual ICollection<Uf> Uf { get; set; } = new List<Uf>();
}