using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICondSecurity.Domain.Models;

public partial class CondSecurityContext : DbContext
{
    public CondSecurityContext()
    {
    }

    public CondSecurityContext(DbContextOptions<CondSecurityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camera> Cameras { get; set; }

    public virtual DbSet<Cidade> Cidades { get; set; }

    public virtual DbSet<Condominio> Condominios { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Notificacao> Notificacaos { get; set; }

    public virtual DbSet<Permissao> Permissaos { get; set; }

    public virtual DbSet<Portao> Portaos { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Residencium> Residencia { get; set; }

    public virtual DbSet<Rfid> Rfids { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Uf> Ufs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    public virtual DbSet<VeiculoTerceiro> VeiculoTerceiros { get; set; }

    public virtual DbSet<VeiculoUsuario> VeiculoUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=cond-security.postgres.database.azure.com;Database=cond-security;Username=condsecurity;Password=TCS2024@;SSL Mode=Require;Persist Security Info=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camera>(entity =>
        {
            entity.HasKey(e => new { e.IdCamera, e.IpCamera }).HasName("cameras_pkey");

            entity.ToTable("cameras");

            entity.Property(e => e.IdCamera)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_camera");
            entity.Property(e => e.IpCamera)
                .HasMaxLength(30)
                .HasColumnName("ip_camera");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.Posicao)
                .HasMaxLength(20)
                .HasColumnName("posicao");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.IdCidade).HasName("cidade_pkey");

            entity.ToTable("cidade");

            entity.Property(e => e.IdCidade).HasColumnName("id_cidade");
            entity.Property(e => e.CidadeIbge).HasColumnName("cidade_ibge");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Condominio>(entity =>
        {
            entity.HasKey(e => e.IdCondominio).HasName("condominio_pkey");

            entity.ToTable("condominio");

            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Situacao)
                .HasMaxLength(1)
                .HasColumnName("situacao");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.IdEndereco).HasName("endereco_pkey");

            entity.ToTable("endereco");

            entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");
            entity.Property(e => e.Bairro)
                .HasMaxLength(30)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(9)
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnName("complemento");
            entity.Property(e => e.IdUf).HasColumnName("id_uf");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Rua)
                .HasMaxLength(30)
                .HasColumnName("rua");
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.IdNotificacao).HasName("notificacao_pkey");

            entity.ToTable("notificacao");

            entity.Property(e => e.IdNotificacao)
                .HasDefaultValueSql("nextval('notificacao_id_notificao_seq'::regclass)")
                .HasColumnName("id_notificacao");
            entity.Property(e => e.DataHora)
                .HasPrecision(6)
                .HasColumnName("data_hora");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .HasColumnName("imagem");
            entity.Property(e => e.Mensagem).HasColumnName("mensagem");
            entity.Property(e => e.Situacao)
                .HasMaxLength(1)
                .HasColumnName("situacao");
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.IdPermissao).HasName("permissao_pkey");

            entity.ToTable("permissao");

            entity.Property(e => e.IdPermissao).HasColumnName("id_permissao");
            entity.Property(e => e.IdNotificacao).HasColumnName("id_notificacao");
            entity.Property(e => e.Situacao)
                .HasMaxLength(1)
                .HasColumnName("situacao");
        });

        modelBuilder.Entity<Portao>(entity =>
        {
            entity.HasKey(e => e.IdPortao).HasName("portao_pkey");

            entity.ToTable("portao");

            entity.Property(e => e.IdPortao).HasColumnName("id_portao");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistros).HasName("registros_pkey");

            entity.ToTable("registros");

            entity.Property(e => e.IdRegistros).HasColumnName("id_registros");
            entity.Property(e => e.DataHoraEntrada)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("data_hora_entrada");
            entity.Property(e => e.DataHoraSaida)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("data_hora_saida");
            entity.Property(e => e.IdPortao).HasColumnName("id_portao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
            entity.Property(e => e.IdVeiculoTerceiro).HasColumnName("id_veiculo_terceiro");
            entity.Property(e => e.IdVeiculoUsuario).HasColumnName("id_veiculo_usuario");
            entity.Property(e => e.Placa)
                .HasMaxLength(255)
                .HasColumnName("placa");
        });

        modelBuilder.Entity<Residencium>(entity =>
        {
            entity.HasKey(e => e.IdResidencia).HasName("residencia_pkey");

            entity.ToTable("residencia");

            entity.Property(e => e.IdResidencia).HasColumnName("id_residencia");
            entity.Property(e => e.Bloco)
                .HasMaxLength(20)
                .HasColumnName("bloco");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Quadra)
                .HasMaxLength(20)
                .HasColumnName("quadra");
            entity.Property(e => e.Rua)
                .HasMaxLength(50)
                .HasColumnName("rua");
        });

        modelBuilder.Entity<Rfid>(entity =>
        {
            entity.HasKey(e => e.IdRfid).HasName("RFID_pkey");

            entity.ToTable("RFID");

            entity.Property(e => e.IdRfid).HasColumnName("id_RFID");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Situacao)
                .HasMaxLength(1)
                .HasColumnName("situacao");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("tipo_usuario_pkey");

            entity.ToTable("tipo_usuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
            entity.Property(e => e.Tipo)
                .HasMaxLength(15)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Uf>(entity =>
        {
            entity.HasKey(e => e.IdUf).HasName("uf_pkey");

            entity.ToTable("uf");

            entity.Property(e => e.IdUf).HasColumnName("id_uf");
            entity.Property(e => e.IdCidade).HasColumnName("id_cidade");
            entity.Property(e => e.Nome)
                .HasMaxLength(30)
                .HasColumnName("nome");
            entity.Property(e => e.Sigla)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("sigla");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.IdResidencia).HasColumnName("id_residencia");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
            entity.Property(e => e.Situacao)
                .HasMaxLength(2)
                .HasColumnName("situacao");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => new { e.IdVeiculo, e.Placa }).HasName("veiculo_pkey");

            entity.ToTable("veiculo");

            entity.Property(e => e.IdVeiculo)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_veiculo");
            entity.Property(e => e.Placa)
                .HasMaxLength(255)
                .HasColumnName("placa");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Cor)
                .HasMaxLength(20)
                .HasColumnName("cor");
            entity.Property(e => e.IdCondominio).HasColumnName("id_condominio");
            entity.Property(e => e.Marca)
                .HasMaxLength(20)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(20)
                .HasColumnName("modelo");
            entity.Property(e => e.Situacao)
                .HasMaxLength(2)
                .HasColumnName("situacao");
        });

        modelBuilder.Entity<VeiculoTerceiro>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("veiculo_terceiro");

            entity.Property(e => e.IdNotificacao).HasColumnName("id_notificacao");
            entity.Property(e => e.IdPermissao).HasColumnName("id_permissao");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
            entity.Property(e => e.IdVeiculoTerceiro)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_veiculo_terceiro");
            entity.Property(e => e.Placa)
                .HasMaxLength(255)
                .HasColumnName("placa");

            entity.HasOne(d => d.IdNotificacaoNavigation).WithMany()
                .HasForeignKey(d => d.IdNotificacao)
                .HasConstraintName("veiculo_terceiro_id_notificacao_fkey");

            entity.HasOne(d => d.IdPermissaoNavigation).WithMany()
                .HasForeignKey(d => d.IdPermissao)
                .HasConstraintName("veiculo_terceiro_id_permissao_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("veiculo_terceiro_id_usuario_fkey");
        });

        modelBuilder.Entity<VeiculoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdVeiculoUsuario).HasName("veiculo_usuario_pkey");

            entity.ToTable("veiculo_usuario");

            entity.Property(e => e.IdVeiculoUsuario).HasColumnName("id_veiculo_usuario");
            entity.Property(e => e.IdRfid).HasColumnName("id_RFID");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdVeiculo).HasColumnName("id_veiculo");
            entity.Property(e => e.Placa)
                .HasMaxLength(255)
                .HasColumnName("placa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
