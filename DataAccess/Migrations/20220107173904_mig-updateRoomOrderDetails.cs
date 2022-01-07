using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class migupdateRoomOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccessFulPayment",
                table: "RoomOrderDetails",
                newName: "IsPaymentSuccessful");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPaymentSuccessful",
                table: "RoomOrderDetails",
                newName: "IsSuccessFulPayment");
        }
    }
}
