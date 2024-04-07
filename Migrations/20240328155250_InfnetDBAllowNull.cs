using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetMVC.Migrations
{
    /// <inheritdoc />
    public partial class InfnetDBAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Funcionario",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Funcionario",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_DepartamentoId",
                table: "Funcionario",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
