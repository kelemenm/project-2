using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkkrMintavetel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkkrMintavetelStatusz = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Leiras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkkrMintavetel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HumviFelelos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Felelos = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nev = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cim = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumviFelelos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HumviModul",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulKod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Leiras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumviModul", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mertekegyseg",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Megyseg = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HumviLeiras = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SajatLeiras = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mertekegyseg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mintavevo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MintavevoAzonosito = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MvAkkrSzam = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nev = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cim = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ErvKezdete = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErvVege = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mintavevo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MvHely",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MvhKod = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NevSajat = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    NevTeljes = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    VizBazis = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    NevRovid = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Telepules = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Tipus = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    HumviRegiNev = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    GPS_N_Y = table.Column<float>(type: "real", nullable: true),
                    GPS_E_X = table.Column<float>(type: "real", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MvHely", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MvOka",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MvOk = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Leiras = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MvOka", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MvTipus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MvTipusNev = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Leiras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MvTipus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParKod = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    HumviLeiras = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    SajatLeiras = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ParamErtek = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    ParamTip = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VizsgaloLabor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Labor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LabAkkrSzam = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nev = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cim = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ErvKezdete = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErvVege = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VizsgaloLabor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Minta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaborMintaKod = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ModulKod = table.Column<long>(type: "bigint", nullable: false),
                    Felelos = table.Column<long>(type: "bigint", nullable: false),
                    MvTipus = table.Column<long>(type: "bigint", nullable: false),
                    MvDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Labor = table.Column<long>(type: "bigint", nullable: false),
                    MintaAtvetel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VizsgalatKezdete = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VizsgalatVege = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MvOk = table.Column<long>(type: "bigint", nullable: false),
                    MvOkaEgyeb = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MvhKod = table.Column<long>(type: "bigint", nullable: false),
                    MvHely = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AkkrMintavetel = table.Column<long>(type: "bigint", nullable: true),
                    Mintavevo = table.Column<long>(type: "bigint", nullable: true),
                    HUMVIexport = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minta_AkkrMintavetel_AkkrMintavetel",
                        column: x => x.AkkrMintavetel,
                        principalTable: "AkkrMintavetel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_HumviFelelos_Felelos",
                        column: x => x.Felelos,
                        principalTable: "HumviFelelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_HumviModul_ModulKod",
                        column: x => x.ModulKod,
                        principalTable: "HumviModul",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_Mintavevo_Mintavevo",
                        column: x => x.Mintavevo,
                        principalTable: "Mintavevo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_MvHely_MvhKod",
                        column: x => x.MvhKod,
                        principalTable: "MvHely",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_MvOka_MvOk",
                        column: x => x.MvOk,
                        principalTable: "MvOka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_MvTipus_MvTipus",
                        column: x => x.MvTipus,
                        principalTable: "MvTipus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minta_VizsgaloLabor_Labor",
                        column: x => x.Labor,
                        principalTable: "VizsgaloLabor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eredmeny",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MintaId = table.Column<long>(type: "bigint", nullable: true),
                    ParKod = table.Column<long>(type: "bigint", nullable: false),
                    Megyseg = table.Column<long>(type: "bigint", nullable: false),
                    Ertek = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AlsoMh = table.Column<float>(type: "real", nullable: true),
                    MaxRange = table.Column<float>(type: "real", nullable: true),
                    ErtekHozzarendelt = table.Column<float>(type: "real", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eredmeny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eredmeny_Mertekegyseg_Megyseg",
                        column: x => x.Megyseg,
                        principalTable: "Mertekegyseg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eredmeny_Minta_MintaId",
                        column: x => x.MintaId,
                        principalTable: "Minta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eredmeny_Parameter_ParKod",
                        column: x => x.ParKod,
                        principalTable: "Parameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkkrMintavetel_AkkrMintavetelStatusz",
                table: "AkkrMintavetel",
                column: "AkkrMintavetelStatusz",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkkrMintavetel_Id",
                table: "AkkrMintavetel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Eredmeny_Id",
                table: "Eredmeny",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Eredmeny_Megyseg",
                table: "Eredmeny",
                column: "Megyseg");

            migrationBuilder.CreateIndex(
                name: "IX_Eredmeny_MintaId",
                table: "Eredmeny",
                column: "MintaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eredmeny_ParKod",
                table: "Eredmeny",
                column: "ParKod");

            migrationBuilder.CreateIndex(
                name: "IX_HumviFelelos_Felelos",
                table: "HumviFelelos",
                column: "Felelos",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HumviFelelos_Id",
                table: "HumviFelelos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HumviModul_Id",
                table: "HumviModul",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HumviModul_ModulKod",
                table: "HumviModul",
                column: "ModulKod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mertekegyseg_Id",
                table: "Mertekegyseg",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mertekegyseg_Megyseg",
                table: "Mertekegyseg",
                column: "Megyseg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minta_AkkrMintavetel",
                table: "Minta",
                column: "AkkrMintavetel");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_Felelos",
                table: "Minta",
                column: "Felelos");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_Id",
                table: "Minta",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_Labor",
                table: "Minta",
                column: "Labor");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_Mintavevo",
                table: "Minta",
                column: "Mintavevo");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_ModulKod",
                table: "Minta",
                column: "ModulKod");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_MvhKod",
                table: "Minta",
                column: "MvhKod");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_MvOk",
                table: "Minta",
                column: "MvOk");

            migrationBuilder.CreateIndex(
                name: "IX_Minta_MvTipus",
                table: "Minta",
                column: "MvTipus");

            migrationBuilder.CreateIndex(
                name: "IX_Mintavevo_Id",
                table: "Mintavevo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mintavevo_MintavevoAzonosito_MvAkkrSzam",
                table: "Mintavevo",
                columns: new[] { "MintavevoAzonosito", "MvAkkrSzam" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MvHely_Id",
                table: "MvHely",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MvHely_MvhKod",
                table: "MvHely",
                column: "MvhKod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MvOka_Id",
                table: "MvOka",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MvOka_MvOk",
                table: "MvOka",
                column: "MvOk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MvTipus_Id",
                table: "MvTipus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MvTipus_MvTipusNev",
                table: "MvTipus",
                column: "MvTipusNev",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_Id",
                table: "Parameter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ParKod",
                table: "Parameter",
                column: "ParKod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VizsgaloLabor_Id",
                table: "VizsgaloLabor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VizsgaloLabor_Labor_LabAkkrSzam",
                table: "VizsgaloLabor",
                columns: new[] { "Labor", "LabAkkrSzam" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eredmeny");

            migrationBuilder.DropTable(
                name: "Mertekegyseg");

            migrationBuilder.DropTable(
                name: "Minta");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "AkkrMintavetel");

            migrationBuilder.DropTable(
                name: "HumviFelelos");

            migrationBuilder.DropTable(
                name: "HumviModul");

            migrationBuilder.DropTable(
                name: "Mintavevo");

            migrationBuilder.DropTable(
                name: "MvHely");

            migrationBuilder.DropTable(
                name: "MvOka");

            migrationBuilder.DropTable(
                name: "MvTipus");

            migrationBuilder.DropTable(
                name: "VizsgaloLabor");
        }
    }
}
