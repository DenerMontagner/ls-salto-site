using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace LSalto.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anuncios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilegios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePrivilegio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequisitoHoras = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilegios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publicadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmailUsername = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataBatismo = table.Column<DateOnly>(type: "date", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDesignacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequerSexoMasculino = table.Column<bool>(type: "bit", nullable: false),
                    RequerCargoEspecifico = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDesignacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnexosAnuncios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnuncio = table.Column<int>(type: "int", nullable: false),
                    CaminhoArquivoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexosAnuncios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnexosAnuncios_Anuncios_IdAnuncio",
                        column: x => x.IdAnuncio,
                        principalTable: "Anuncios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Local = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdAnciaoResponsavel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_Publicadores_IdAnciaoResponsavel",
                        column: x => x.IdAnciaoResponsavel,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PublicadoresCargos",
                columns: table => new
                {
                    IdPublicador = table.Column<int>(type: "int", nullable: false),
                    IdCargo = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicadoresCargos", x => new { x.IdPublicador, x.IdCargo, x.DataInicio });
                    table.ForeignKey(
                        name: "FK_PublicadoresCargos_Cargos_IdCargo",
                        column: x => x.IdCargo,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicadoresCargos_Publicadores_IdPublicador",
                        column: x => x.IdPublicador,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicadoresPrivilegios",
                columns: table => new
                {
                    IdPublicador = table.Column<int>(type: "int", nullable: false),
                    IdPrivilegio = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicadoresPrivilegios", x => new { x.IdPublicador, x.IdPrivilegio, x.DataInicio });
                    table.ForeignKey(
                        name: "FK_PublicadoresPrivilegios_Privilegios_IdPrivilegio",
                        column: x => x.IdPrivilegio,
                        principalTable: "Privilegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicadoresPrivilegios_Publicadores_IdPublicador",
                        column: x => x.IdPublicador,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoDesignacao = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    IdPublicadorTitular = table.Column<int>(type: "int", nullable: false),
                    IdPublicadorAjudante = table.Column<int>(type: "int", nullable: true),
                    IdGrupo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designacoes_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Designacoes_Publicadores_IdPublicadorAjudante",
                        column: x => x.IdPublicadorAjudante,
                        principalTable: "Publicadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Designacoes_Publicadores_IdPublicadorTitular",
                        column: x => x.IdPublicadorTitular,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designacoes_TiposDesignacao_IdTipoDesignacao",
                        column: x => x.IdTipoDesignacao,
                        principalTable: "TiposDesignacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GruposPublicadores",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    IdPublicador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposPublicadores", x => new { x.IdGrupo, x.IdPublicador });
                    table.ForeignKey(
                        name: "FK_GruposPublicadores_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GruposPublicadores_Publicadores_IdPublicador",
                        column: x => x.IdPublicador,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "NomeCargo" },
                values: new object[,]
                {
                    { 1, "Ancião" },
                    { 2, "Servo Ministerial" }
                });

            migrationBuilder.InsertData(
                table: "Privilegios",
                columns: new[] { "Id", "NomePrivilegio", "RequisitoHoras" },
                values: new object[,]
                {
                    { 1, "Pioneiro Regular", 840 },
                    { 2, "Pioneiro Auxiliar", 30 }
                });

            migrationBuilder.InsertData(
                table: "TiposDesignacao",
                columns: new[] { "Id", "Nome", "RequerCargoEspecifico", "RequerSexoMasculino" },
                values: new object[,]
                {
                    { 1, "Presidente", "Anciao", true },
                    { 2, "Estudo Bíblico (EBC)", "Anciao", true },
                    { 3, "Discurso Tesouros", "ServoOuAnciao", true },
                    { 4, "Discurso Vida Cristã", "ServoOuAnciao", true },
                    { 5, "Leitura da Bíblia", "Nenhum", true },
                    { 6, "Mecânicas", "Nenhum", true },
                    { 7, "Oração Inicial", "Nenhum", true },
                    { 8, "Oração Final", "Nenhum", true },
                    { 9, "Demonstração (Titular)", "Nenhum", false },
                    { 10, "Demonstração (Ajudante)", "Nenhum", false },
                    { 11, "Limpeza", "Nenhum", false },
                    { 12, "Pregação de Campo", "Nenhum", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnexosAnuncios_IdAnuncio",
                table: "AnexosAnuncios",
                column: "IdAnuncio");

            migrationBuilder.CreateIndex(
                name: "IX_Designacao_Conflito",
                table: "Designacoes",
                columns: new[] { "IdPublicadorTitular", "Data", "IdTipoDesignacao" });

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_IdGrupo",
                table: "Designacoes",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_IdPublicadorAjudante",
                table: "Designacoes",
                column: "IdPublicadorAjudante");

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_IdTipoDesignacao",
                table: "Designacoes",
                column: "IdTipoDesignacao");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_IdAnciaoResponsavel",
                table: "Grupos",
                column: "IdAnciaoResponsavel");

            migrationBuilder.CreateIndex(
                name: "IX_GruposPublicadores_IdPublicador",
                table: "GruposPublicadores",
                column: "IdPublicador");

            migrationBuilder.CreateIndex(
                name: "IX_Publicadores_EmailUsername",
                table: "Publicadores",
                column: "EmailUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicadoresCargos_IdCargo",
                table: "PublicadoresCargos",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_PublicadoresPrivilegios_IdPrivilegio",
                table: "PublicadoresPrivilegios",
                column: "IdPrivilegio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnexosAnuncios");

            migrationBuilder.DropTable(
                name: "Designacoes");

            migrationBuilder.DropTable(
                name: "GruposPublicadores");

            migrationBuilder.DropTable(
                name: "PublicadoresCargos");

            migrationBuilder.DropTable(
                name: "PublicadoresPrivilegios");

            migrationBuilder.DropTable(
                name: "Anuncios");

            migrationBuilder.DropTable(
                name: "TiposDesignacao");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Privilegios");

            migrationBuilder.DropTable(
                name: "Publicadores");
        }
    }
}
