using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace company_website.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CATEGORY__3214EC27FFE92409", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMPANY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LOCATED = table.Column<string>(type: "text", nullable: true),
                    FACEBOOK = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    X = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PHONE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    FOCUSEDfIELD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COMPANY__3214EC2764E4C18D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ROLE__3214EC27BAE24ACD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "text", nullable: true),
                    TASK_DESCRIPTION = table.Column<string>(type: "text", nullable: true),
                    EXPECTED_END_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    START_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    TEAM_SIZE = table.Column<int>(type: "int", nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    THUMBAIL = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TASK__3214EC2707DAE29F", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMPANY_ID = table.Column<int>(type: "int", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DEPARTME__3214EC2772C264B9", x => x.ID);
                    table.ForeignKey(
                        name: "FK__DEPARTMEN__COMPA__3E52440B",
                        column: x => x.COMPANY_ID,
                        principalTable: "COMPANY",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_ACCOUNT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLE_ID = table.Column<int>(type: "int", nullable: true),
                    USERNAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PASSWORD = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER_ACC__3214EC273CF89E00", x => x.ID);
                    table.ForeignKey(
                        name: "FK__USER_ACCO__PASSW__5441852A",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DEPARTMENT_ID = table.Column<int>(type: "int", nullable: true),
                    USER_ACCOUNT_ID = table.Column<int>(type: "int", nullable: true),
                    FULLNAME = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateOnly>(type: "date", nullable: true),
                    ID_NO = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EMPLOYEE__3214EC2764B462B6", x => x.ID);
                    table.ForeignKey(
                        name: "FK__EMPLOYEE__DEPART__5812160E",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENT",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__EMPLOYEE__USER_A__59063A47",
                        column: x => x.USER_ACCOUNT_ID,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "POST",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_ID = table.Column<int>(type: "int", nullable: true),
                    USER_ACCOUNT_ID = table.Column<int>(type: "int", nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    THUMBNAIL = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CONTENT = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POST__3214EC275D10D1B3", x => x.ID);
                    table.ForeignKey(
                        name: "FK__POST__CATEGORY_I__5BE2A6F2",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORY",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__POST__USER_ACCOU__5CD6CB2B",
                        column: x => x.USER_ACCOUNT_ID,
                        principalTable: "USER_ACCOUNT",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SCHEDULES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMPLOYEE_ID = table.Column<int>(type: "int", nullable: true),
                    TASK_ID = table.Column<int>(type: "int", nullable: true),
                    DETAILS_TASK = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ID_NO = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SCHEDULE__3214EC27264FB104", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SCHEDULES__EMPLO__5FB337D6",
                        column: x => x.EMPLOYEE_ID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__SCHEDULES__TASK___60A75C0F",
                        column: x => x.TASK_ID,
                        principalTable: "TASK",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DEPARTMENT_COMPANY_ID",
                table: "DEPARTMENT",
                column: "COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_DEPARTMENT_ID",
                table: "EMPLOYEE",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__EMPLOYEE__56453098A3D2F443",
                table: "EMPLOYEE",
                column: "USER_ACCOUNT_ID",
                unique: true,
                filter: "[USER_ACCOUNT_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_POST_CATEGORY_ID",
                table: "POST",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_USER_ACCOUNT_ID",
                table: "POST",
                column: "USER_ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_EMPLOYEE_ID",
                table: "SCHEDULES",
                column: "EMPLOYEE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_TASK_ID",
                table: "SCHEDULES",
                column: "TASK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ACCOUNT_ROLE_ID",
                table: "USER_ACCOUNT",
                column: "ROLE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POST");

            migrationBuilder.DropTable(
                name: "SCHEDULES");

            migrationBuilder.DropTable(
                name: "CATEGORY");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "DEPARTMENT");

            migrationBuilder.DropTable(
                name: "USER_ACCOUNT");

            migrationBuilder.DropTable(
                name: "COMPANY");

            migrationBuilder.DropTable(
                name: "ROLE");
        }
    }
}
