using System;
using LSalto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LSalto.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260523180522_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LSalto.Domain.Entities.AnexoAnuncio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CaminhoArquivoUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("IdAnuncio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAnuncio");

                    b.ToTable("AnexosAnuncios");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Anuncio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.ToTable("Anuncios");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomeCargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cargos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NomeCargo = "Ancião"
                        },
                        new
                        {
                            Id = 2,
                            NomeCargo = "Servo Ministerial"
                        });
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Designacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date");

                    b.Property<int?>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int?>("IdPublicadorAjudante")
                        .HasColumnType("int");

                    b.Property<int>("IdPublicadorTitular")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoDesignacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdGrupo");

                    b.HasIndex("IdPublicadorAjudante");

                    b.HasIndex("IdTipoDesignacao");

                    b.HasIndex("IdPublicadorTitular", "Data", "IdTipoDesignacao")
                        .HasDatabaseName("IX_Designacao_Conflito");

                    b.ToTable("Designacoes");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdAnciaoResponsavel")
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdAnciaoResponsavel");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.GrupoPublicador", b =>
                {
                    b.Property<int>("IdGrupo")
                        .HasColumnType("int");

                    b.Property<int>("IdPublicador")
                        .HasColumnType("int");

                    b.HasKey("IdGrupo", "IdPublicador");

                    b.HasIndex("IdPublicador");

                    b.ToTable("GruposPublicadores");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Privilegio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomePrivilegio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequisitoHoras")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Privilegios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NomePrivilegio = "Pioneiro Regular",
                            RequisitoHoras = 840
                        },
                        new
                        {
                            Id = 2,
                            NomePrivilegio = "Pioneiro Auxiliar",
                            RequisitoHoras = 30
                        });
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Publicador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("DataBatismo")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("EmailUsername")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("EmailUsername")
                        .IsUnique();

                    b.ToTable("Publicadores");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.PublicadorCargo", b =>
                {
                    b.Property<int>("IdPublicador")
                        .HasColumnType("int");

                    b.Property<int>("IdCargo")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataInicio")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DataFim")
                        .HasColumnType("date");

                    b.HasKey("IdPublicador", "IdCargo", "DataInicio");

                    b.HasIndex("IdCargo");

                    b.ToTable("PublicadoresCargos");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.PublicadorPrivilegio", b =>
                {
                    b.Property<int>("IdPublicador")
                        .HasColumnType("int");

                    b.Property<int>("IdPrivilegio")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataInicio")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DataFim")
                        .HasColumnType("date");

                    b.HasKey("IdPublicador", "IdPrivilegio", "DataInicio");

                    b.HasIndex("IdPrivilegio");

                    b.ToTable("PublicadoresPrivilegios");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.TipoDesignacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RequerCargoEspecifico")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("RequerSexoMasculino")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TiposDesignacao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Presidente",
                            RequerCargoEspecifico = "Anciao",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Estudo Bíblico (EBC)",
                            RequerCargoEspecifico = "Anciao",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Discurso Tesouros",
                            RequerCargoEspecifico = "ServoOuAnciao",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Discurso Vida Cristã",
                            RequerCargoEspecifico = "ServoOuAnciao",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Leitura da Bíblia",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Mecânicas",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 7,
                            Nome = "Oração Inicial",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 8,
                            Nome = "Oração Final",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = true
                        },
                        new
                        {
                            Id = 9,
                            Nome = "Demonstração (Titular)",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = false
                        },
                        new
                        {
                            Id = 10,
                            Nome = "Demonstração (Ajudante)",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = false
                        },
                        new
                        {
                            Id = 11,
                            Nome = "Limpeza",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = false
                        },
                        new
                        {
                            Id = 12,
                            Nome = "Pregação de Campo",
                            RequerCargoEspecifico = "Nenhum",
                            RequerSexoMasculino = false
                        });
                });

            modelBuilder.Entity("LSalto.Domain.Entities.AnexoAnuncio", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Anuncio", "Anuncio")
                        .WithMany("Anexos")
                        .HasForeignKey("IdAnuncio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anuncio");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Designacao", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Grupo", "Grupo")
                        .WithMany("Designacoes")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LSalto.Domain.Entities.Publicador", "PublicadorAjudante")
                        .WithMany("DesignacoesComoAjudante")
                        .HasForeignKey("IdPublicadorAjudante")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LSalto.Domain.Entities.Publicador", "PublicadorTitular")
                        .WithMany("DesignacoesComoTitular")
                        .HasForeignKey("IdPublicadorTitular")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LSalto.Domain.Entities.TipoDesignacao", "TipoDesignacao")
                        .WithMany("Designacoes")
                        .HasForeignKey("IdTipoDesignacao")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grupo");

                    b.Navigation("PublicadorAjudante");

                    b.Navigation("PublicadorTitular");

                    b.Navigation("TipoDesignacao");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Grupo", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Publicador", "AnciaoResponsavel")
                        .WithMany()
                        .HasForeignKey("IdAnciaoResponsavel")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AnciaoResponsavel");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.GrupoPublicador", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Grupo", "Grupo")
                        .WithMany("Publicadores")
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LSalto.Domain.Entities.Publicador", "Publicador")
                        .WithMany("Grupos")
                        .HasForeignKey("IdPublicador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grupo");

                    b.Navigation("Publicador");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.PublicadorCargo", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Cargo", "Cargo")
                        .WithMany("Publicadores")
                        .HasForeignKey("IdCargo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LSalto.Domain.Entities.Publicador", "Publicador")
                        .WithMany("Cargos")
                        .HasForeignKey("IdPublicador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Publicador");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.PublicadorPrivilegio", b =>
                {
                    b.HasOne("LSalto.Domain.Entities.Privilegio", "Privilegio")
                        .WithMany("Publicadores")
                        .HasForeignKey("IdPrivilegio")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LSalto.Domain.Entities.Publicador", "Publicador")
                        .WithMany("Privilegios")
                        .HasForeignKey("IdPublicador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Privilegio");

                    b.Navigation("Publicador");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Anuncio", b =>
                {
                    b.Navigation("Anexos");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Cargo", b =>
                {
                    b.Navigation("Publicadores");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Grupo", b =>
                {
                    b.Navigation("Designacoes");

                    b.Navigation("Publicadores");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Privilegio", b =>
                {
                    b.Navigation("Publicadores");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.Publicador", b =>
                {
                    b.Navigation("Cargos");

                    b.Navigation("DesignacoesComoAjudante");

                    b.Navigation("DesignacoesComoTitular");

                    b.Navigation("Grupos");

                    b.Navigation("Privilegios");
                });

            modelBuilder.Entity("LSalto.Domain.Entities.TipoDesignacao", b =>
                {
                    b.Navigation("Designacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
