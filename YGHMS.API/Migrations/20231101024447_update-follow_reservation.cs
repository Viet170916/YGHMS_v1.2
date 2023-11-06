using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YGHMS.API.Migrations
{
    /// <inheritdoc />
    public partial class updatefollow_reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_reservation_Notification",
                table: "Notification_reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_reservation_Notification",
                table: "Notification_reservation",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_reservation_Notification",
                table: "Notification_reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_reservation_Notification",
                table: "Notification_reservation",
                column: "NotificationId",
                principalTable: "Notification_reservation",
                principalColumn: "Id");
        }
    }
}
