﻿// <auto-generated />
using System;
using APICondSecurity.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICondSecurity.Migrations
{
    [DbContext(typeof(condSecurityContext))]
    [Migration("20240406002943_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APICondSecurity.Models.Cameras", b =>
                {
                    b.Property<int>("IdCamera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_camera");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCamera"));

                    b.Property<string>("IpCamera")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("ip_camera");

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<string>("Posicao")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("posicao");

                    b.Property<string>("Tipo")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("tipo");

                    b.HasKey("IdCamera", "IpCamera")
                        .HasName("cameras_pkey");

                    b.HasIndex("IdCondominio");

                    b.ToTable("cameras");
                });

            modelBuilder.Entity("APICondSecurity.Models.Cidade", b =>
                {
                    b.Property<int>("IdCidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_cidade");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCidade"));

                    b.Property<int>("CidadeIbge")
                        .HasColumnType("integer")
                        .HasColumnName("cidade_ibge");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nome");

                    b.HasKey("IdCidade")
                        .HasName("cidade_pkey");

                    b.ToTable("cidade");
                });

            modelBuilder.Entity("APICondSecurity.Models.Condominio", b =>
                {
                    b.Property<int>("IdCondominio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCondominio"));

                    b.Property<int>("IdEndereco")
                        .HasColumnType("integer")
                        .HasColumnName("id_endereco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nome");

                    b.Property<char>("Situacao")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("situacao");

                    b.HasKey("IdCondominio")
                        .HasName("condominio_pkey");

                    b.HasIndex("IdEndereco");

                    b.ToTable("condominio");
                });

            modelBuilder.Entity("APICondSecurity.Models.Endereco", b =>
                {
                    b.Property<int>("IdEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_endereco");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdEndereco"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("bairro");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)")
                        .HasColumnName("cep");

                    b.Property<string>("Complemento")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("complemento");

                    b.Property<int>("IdUf")
                        .HasColumnType("integer")
                        .HasColumnName("id_uf");

                    b.Property<int?>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("rua");

                    b.HasKey("IdEndereco")
                        .HasName("endereco_pkey");

                    b.HasIndex("IdUf");

                    b.ToTable("endereco");
                });

            modelBuilder.Entity("APICondSecurity.Models.Notificacao", b =>
                {
                    b.Property<int>("IdNotificacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_notificacao");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdNotificacao"));

                    b.Property<TimeOnly>("DataHora")
                        .HasPrecision(6)
                        .HasColumnType("time(6) without time zone")
                        .HasColumnName("data_hora");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("imagem");

                    b.Property<string>("Mensagem")
                        .HasColumnType("text")
                        .HasColumnName("mensagem");

                    b.Property<char?>("Situacao")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("situacao");

                    b.Property<char?>("Tipo")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("tipo");

                    b.HasKey("IdNotificacao")
                        .HasName("notificacao_pkey");

                    b.HasIndex("IdUsuario");

                    b.ToTable("notificacao");
                });

            modelBuilder.Entity("APICondSecurity.Models.Permissao", b =>
                {
                    b.Property<int>("IdPermissao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_permissao");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPermissao"));

                    b.Property<int>("IdNotificacao")
                        .HasColumnType("integer")
                        .HasColumnName("id_notificacao");

                    b.Property<char>("Situacao")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("situacao");

                    b.HasKey("IdPermissao")
                        .HasName("permissao_pkey");

                    b.HasIndex("IdNotificacao");

                    b.ToTable("permissao");
                });

            modelBuilder.Entity("APICondSecurity.Models.Portao", b =>
                {
                    b.Property<int>("IdPortao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_portao");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPortao"));

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nome");

                    b.HasKey("IdPortao")
                        .HasName("portao_pkey");

                    b.HasIndex("IdCondominio");

                    b.ToTable("portao");
                });

            modelBuilder.Entity("APICondSecurity.Models.Registros", b =>
                {
                    b.Property<int>("IdRegistros")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_registros");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRegistros"));

                    b.Property<DateTime?>("DataHoraEntrada")
                        .HasColumnType("timestamp(6) without time zone")
                        .HasColumnName("data_hora_entrada");

                    b.Property<DateTime?>("DataHoraSaida")
                        .HasColumnType("timestamp(6) without time zone")
                        .HasColumnName("data_hora_saida");

                    b.Property<int>("IdPortao")
                        .HasColumnType("integer")
                        .HasColumnName("id_portao");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.Property<int?>("IdVeiculo")
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo");

                    b.Property<int?>("IdVeiculoTerceiro")
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo_terceiro");

                    b.Property<int?>("IdVeiculoUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo_usuario");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("placa");

                    b.HasKey("IdRegistros")
                        .HasName("registros_pkey");

                    b.HasIndex("IdPortao");

                    b.HasIndex("IdVeiculoTerceiro");

                    b.HasIndex("IdVeiculoUsuario");

                    b.ToTable("registros");
                });

            modelBuilder.Entity("APICondSecurity.Models.Residencia", b =>
                {
                    b.Property<int>("IdResidencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_residencia");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdResidencia"));

                    b.Property<string>("Bloco")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("bloco");

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<string>("Quadra")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("quadra");

                    b.Property<string>("Rua")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("rua");

                    b.HasKey("IdResidencia")
                        .HasName("residencia_pkey");

                    b.HasIndex("IdCondominio");

                    b.ToTable("residencia");
                });

            modelBuilder.Entity("APICondSecurity.Models.Rfid", b =>
                {
                    b.Property<int>("IdRfid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_RFID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRfid"));

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<char>("Situacao")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("situacao");

                    b.HasKey("IdRfid")
                        .HasName("RFID_pkey");

                    b.HasIndex("IdCondominio");

                    b.ToTable("RFID");
                });

            modelBuilder.Entity("APICondSecurity.Models.TipoUsuario", b =>
                {
                    b.Property<int>("IdTipoUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_tipo_usuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdTipoUsuario"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("tipo");

                    b.HasKey("IdTipoUsuario")
                        .HasName("tipo_usuario_pkey");

                    b.ToTable("tipo_usuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.Uf", b =>
                {
                    b.Property<int>("IdUf")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_uf");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUf"));

                    b.Property<int>("IdCidade")
                        .HasColumnType("integer")
                        .HasColumnName("id_cidade");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("nome");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character(2)")
                        .HasColumnName("sigla")
                        .IsFixedLength();

                    b.HasKey("IdUf")
                        .HasName("uf_pkey");

                    b.HasIndex("IdCidade");

                    b.ToTable("uf");
                });

            modelBuilder.Entity("APICondSecurity.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<int>("IdResidencia")
                        .HasColumnType("integer")
                        .HasColumnName("id_residencia");

                    b.Property<int>("IdTipoUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_tipo_usuario");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("senha");

                    b.Property<string>("Situacao")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("situacao");

                    b.Property<string>("Telefone")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("telefone");

                    b.HasKey("IdUsuario")
                        .HasName("usuario_pkey");

                    b.HasIndex("IdCondominio");

                    b.HasIndex("IdResidencia");

                    b.HasIndex("IdTipoUsuario");

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.Veiculo", b =>
                {
                    b.Property<int>("IdVeiculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVeiculo"));

                    b.Property<string>("Placa")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("placa");

                    b.Property<int>("Ano")
                        .HasColumnType("integer")
                        .HasColumnName("ano");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("cor");

                    b.Property<int>("IdCondominio")
                        .HasColumnType("integer")
                        .HasColumnName("id_condominio");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("marca");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("modelo");

                    b.Property<string>("Situacao")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("situacao");

                    b.HasKey("IdVeiculo", "Placa")
                        .HasName("veiculo_pkey");

                    b.HasIndex("IdCondominio");

                    b.ToTable("veiculo");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoTerceiro", b =>
                {
                    b.Property<int>("IdVeiculoTerceiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo_terceiro");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVeiculoTerceiro"));

                    b.Property<int?>("IdNotificacao")
                        .HasColumnType("integer")
                        .HasColumnName("id_notificacao");

                    b.Property<int?>("IdPermissao")
                        .HasColumnType("integer")
                        .HasColumnName("id_permissao");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.Property<int?>("IdVeiculo")
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("placa");

                    b.HasKey("IdVeiculoTerceiro")
                        .HasName("veiculo_terceiro_pkey");

                    b.HasIndex("IdNotificacao");

                    b.HasIndex("IdPermissao");

                    b.HasIndex("IdUsuario");

                    b.ToTable("veiculo_terceiro");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoUsuario", b =>
                {
                    b.Property<int>("IdVeiculoUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo_usuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVeiculoUsuario"));

                    b.Property<int>("IdRfid")
                        .HasColumnType("integer")
                        .HasColumnName("id_RFID");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.Property<int?>("IdVeiculo")
                        .HasColumnType("integer")
                        .HasColumnName("id_veiculo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("placa");

                    b.HasKey("IdVeiculoUsuario")
                        .HasName("veiculo_usuario_pkey");

                    b.HasIndex("IdRfid");

                    b.HasIndex("IdUsuario");

                    b.ToTable("veiculo_usuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.Cameras", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Cameras")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("cameras_id_condominio_fkey");

                    b.Navigation("IdCondominioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Condominio", b =>
                {
                    b.HasOne("APICondSecurity.Models.Endereco", "IdEnderecoNavigation")
                        .WithMany("Condominio")
                        .HasForeignKey("IdEndereco")
                        .IsRequired()
                        .HasConstraintName("condominio_id_endereco_fkey");

                    b.Navigation("IdEnderecoNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Endereco", b =>
                {
                    b.HasOne("APICondSecurity.Models.Uf", "IdUfNavigation")
                        .WithMany("Endereco")
                        .HasForeignKey("IdUf")
                        .IsRequired()
                        .HasConstraintName("endereco_id_uf_fkey");

                    b.Navigation("IdUfNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Notificacao", b =>
                {
                    b.HasOne("APICondSecurity.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("Notificacao")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("notificacao_id_usuario_fkey");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Permissao", b =>
                {
                    b.HasOne("APICondSecurity.Models.Notificacao", "IdNotificacaoNavigation")
                        .WithMany("Permissao")
                        .HasForeignKey("IdNotificacao")
                        .IsRequired()
                        .HasConstraintName("permissao_id_notificacao_fkey");

                    b.Navigation("IdNotificacaoNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Portao", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Portao")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("portao_id_condominio_fkey");

                    b.Navigation("IdCondominioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Registros", b =>
                {
                    b.HasOne("APICondSecurity.Models.Portao", "IdPortaoNavigation")
                        .WithMany("Registros")
                        .HasForeignKey("IdPortao")
                        .IsRequired()
                        .HasConstraintName("portao");

                    b.HasOne("APICondSecurity.Models.VeiculoTerceiro", "IdVeiculoTerceiroNavigation")
                        .WithMany("Registros")
                        .HasForeignKey("IdVeiculoTerceiro")
                        .HasConstraintName("registros_id_veiculo_terceiro_fkey");

                    b.HasOne("APICondSecurity.Models.VeiculoUsuario", "IdVeiculoUsuarioNavigation")
                        .WithMany("Registros")
                        .HasForeignKey("IdVeiculoUsuario")
                        .HasConstraintName("registros_id_veiculo_usuario_fkey");

                    b.Navigation("IdPortaoNavigation");

                    b.Navigation("IdVeiculoTerceiroNavigation");

                    b.Navigation("IdVeiculoUsuarioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Residencia", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Residencia")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("residencia_id_condominio_fkey");

                    b.Navigation("IdCondominioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Rfid", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Rfid")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("RFID_id_condominio_fkey");

                    b.Navigation("IdCondominioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Uf", b =>
                {
                    b.HasOne("APICondSecurity.Models.Cidade", "IdCidadeNavigation")
                        .WithMany("Uf")
                        .HasForeignKey("IdCidade")
                        .IsRequired()
                        .HasConstraintName("uf_id_cidade_fkey");

                    b.Navigation("IdCidadeNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Usuario", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Usuario")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("usuario_id_condominio_fkey");

                    b.HasOne("APICondSecurity.Models.Residencia", "IdResidenciaNavigation")
                        .WithMany("Usuario")
                        .HasForeignKey("IdResidencia")
                        .IsRequired()
                        .HasConstraintName("usuario_id_residencia_fkey");

                    b.HasOne("APICondSecurity.Models.TipoUsuario", "IdTipoUsuarioNavigation")
                        .WithMany("Usuario")
                        .HasForeignKey("IdTipoUsuario")
                        .IsRequired()
                        .HasConstraintName("usuario_id_tipo_usuario_fkey");

                    b.Navigation("IdCondominioNavigation");

                    b.Navigation("IdResidenciaNavigation");

                    b.Navigation("IdTipoUsuarioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Veiculo", b =>
                {
                    b.HasOne("APICondSecurity.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Veiculo")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("veiculo_id_condominio_fkey");

                    b.Navigation("IdCondominioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoTerceiro", b =>
                {
                    b.HasOne("APICondSecurity.Models.Notificacao", "IdNotificacaoNavigation")
                        .WithMany("VeiculoTerceiro")
                        .HasForeignKey("IdNotificacao")
                        .HasConstraintName("veiculo_terceiro_id_notificacao_fkey");

                    b.HasOne("APICondSecurity.Models.Permissao", "IdPermissaoNavigation")
                        .WithMany("VeiculoTerceiro")
                        .HasForeignKey("IdPermissao")
                        .HasConstraintName("veiculo_terceiro_id_permissao_fkey");

                    b.HasOne("APICondSecurity.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("VeiculoTerceiro")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("veiculo_terceiro_id_usuario_fkey");

                    b.Navigation("IdNotificacaoNavigation");

                    b.Navigation("IdPermissaoNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoUsuario", b =>
                {
                    b.HasOne("APICondSecurity.Models.Rfid", "IdRf")
                        .WithMany("VeiculoUsuario")
                        .HasForeignKey("IdRfid")
                        .IsRequired()
                        .HasConstraintName("veiculo_usuario_id_RFID_fkey");

                    b.HasOne("APICondSecurity.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("VeiculoUsuario")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("veiculo_usuario_id_usuario_fkey");

                    b.Navigation("IdRf");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("APICondSecurity.Models.Cidade", b =>
                {
                    b.Navigation("Uf");
                });

            modelBuilder.Entity("APICondSecurity.Models.Condominio", b =>
                {
                    b.Navigation("Cameras");

                    b.Navigation("Portao");

                    b.Navigation("Residencia");

                    b.Navigation("Rfid");

                    b.Navigation("Usuario");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("APICondSecurity.Models.Endereco", b =>
                {
                    b.Navigation("Condominio");
                });

            modelBuilder.Entity("APICondSecurity.Models.Notificacao", b =>
                {
                    b.Navigation("Permissao");

                    b.Navigation("VeiculoTerceiro");
                });

            modelBuilder.Entity("APICondSecurity.Models.Permissao", b =>
                {
                    b.Navigation("VeiculoTerceiro");
                });

            modelBuilder.Entity("APICondSecurity.Models.Portao", b =>
                {
                    b.Navigation("Registros");
                });

            modelBuilder.Entity("APICondSecurity.Models.Residencia", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.Rfid", b =>
                {
                    b.Navigation("VeiculoUsuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.TipoUsuario", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.Uf", b =>
                {
                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("APICondSecurity.Models.Usuario", b =>
                {
                    b.Navigation("Notificacao");

                    b.Navigation("VeiculoTerceiro");

                    b.Navigation("VeiculoUsuario");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoTerceiro", b =>
                {
                    b.Navigation("Registros");
                });

            modelBuilder.Entity("APICondSecurity.Models.VeiculoUsuario", b =>
                {
                    b.Navigation("Registros");
                });
#pragma warning restore 612, 618
        }
    }
}