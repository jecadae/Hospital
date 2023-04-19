using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class rename_Schedule_To_Configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "TimeTableId",
                table: "Doctors",
                newName: "ScheduleId");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId1",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId1",
                table: "Schedules",
                column: "DoctorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Doctors_DoctorId1",
                table: "Schedules",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Doctors_DoctorId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_DoctorId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Doctors",
                newName: "TimeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                table: "Schedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
