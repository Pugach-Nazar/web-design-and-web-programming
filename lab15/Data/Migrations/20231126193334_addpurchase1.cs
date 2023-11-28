using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab15.Data.Migrations
{
    public partial class addpurchase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Devices_DeviceId1",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_DeviceId1",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "DeviceId1",
                table: "Purchase");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_DeviceId",
                table: "Purchase",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Devices_DeviceId",
                table: "Purchase",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Devices_DeviceId",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_DeviceId",
                table: "Purchase");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Purchase",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId1",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_DeviceId1",
                table: "Purchase",
                column: "DeviceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Devices_DeviceId1",
                table: "Purchase",
                column: "DeviceId1",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
