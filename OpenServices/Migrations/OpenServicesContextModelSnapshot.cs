﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using OpenServices.Entities;
using System;

namespace OpenServices.Migrations
{
    [DbContext(typeof(OpenServicesContext))]
    partial class OpenServicesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpenServices.Entities.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("OpenServices.Entities.CategoriaPrestador", b =>
                {
                    b.Property<int>("IdCategoriaPrestador")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCategoria");

                    b.Property<int>("IdUsuario");

                    b.HasKey("IdCategoriaPrestador");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdUsuario");

                    b.ToTable("CategoriaPrestadors");
                });

            modelBuilder.Entity("OpenServices.Entities.FormaPagamento", b =>
                {
                    b.Property<int>("IdFormaPagamento")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PrestadorServicoIdUsuario");

                    b.Property<string>("Tipo");

                    b.HasKey("IdFormaPagamento");

                    b.HasIndex("PrestadorServicoIdUsuario");

                    b.ToTable("FormaPagamentos");
                });

            modelBuilder.Entity("OpenServices.Entities.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpf");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Rg");

                    b.Property<string>("Senha");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("OpenServices.Entities.Cliente", b =>
                {
                    b.HasBaseType("OpenServices.Entities.Usuario");

                    b.Property<string>("Endereco");

                    b.ToTable("Cliente");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("OpenServices.Entities.PrestadorServico", b =>
                {
                    b.HasBaseType("OpenServices.Entities.Usuario");

                    b.Property<string>("Cnpj");

                    b.Property<string>("Especializacao");

                    b.ToTable("PrestadorServico");

                    b.HasDiscriminator().HasValue("PrestadorServico");
                });

            modelBuilder.Entity("OpenServices.Entities.CategoriaPrestador", b =>
                {
                    b.HasOne("OpenServices.Entities.Categoria", "Categoria")
                        .WithMany("PrestadoresServico")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenServices.Entities.PrestadorServico", "PrestadorServico")
                        .WithMany("Categorias")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenServices.Entities.FormaPagamento", b =>
                {
                    b.HasOne("OpenServices.Entities.PrestadorServico")
                        .WithMany("FormaPagamentos")
                        .HasForeignKey("PrestadorServicoIdUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
