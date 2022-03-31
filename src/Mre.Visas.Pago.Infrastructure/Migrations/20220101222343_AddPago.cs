using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mre.Visas.Pago.Infraestructure.Migrations
{
    public partial class AddPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTramite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaPago = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", maxLength: 250, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PagoDetalle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTramite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPago = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IdArancel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoArancel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorArancel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentajeDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrdenPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroTransaccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ComprobantePago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoDetalle", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "PagoDetalle");
        }
    }
}
