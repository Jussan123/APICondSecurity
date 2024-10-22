﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Infra.Data.Models;

[Table("condominio")]
public partial class Condominio
{
    [Key]
    [Column("id_condominio")]
    public int IdCondominio { get; set; }

    [Required]
    [Column("nome")]
    [StringLength(50)]
    public string Nome { get; set; }

    [Column("situacao")]
    [StringLength(2)]
    public string Situacao { get; set; }

    [Column("id_endereco")]
    public int IdEndereco { get; set; }

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Cameras> Cameras { get; set; } = new List<Cameras>();

    [ForeignKey("IdEndereco")]
    [InverseProperty("Condominio")]
    public virtual Endereco IdEnderecoNavigation { get; set; }

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Portao> Portao { get; set; } = new List<Portao>();

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Residencia> Residencia { get; set; } = new List<Residencia>();

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Rfid> Rfid { get; set; } = new List<Rfid>();

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();

    [InverseProperty("IdCondominioNavigation")]
    public virtual ICollection<Veiculo> Veiculo { get; set; } = new List<Veiculo>();
}