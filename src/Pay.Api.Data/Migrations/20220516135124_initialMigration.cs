using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pay.Api.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Document = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    Address = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userSubscription_subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userSubscription_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "subscription",
                columns: new[] { "Id", "Address", "CreateDate", "CreateUserId", "DeleteDate", "DeleteUserId", "Deleted", "Document", "Email", "Name", "Phone", "UpdateDate", "UpdateUserId", "ZipCode" },
                values: new object[] { new Guid("e6a9312b-3b0c-480e-9af5-0a5b76817194"), null, new DateTime(2022, 5, 15, 21, 21, 46, 0, DateTimeKind.Unspecified), null, null, null, null, null, null, "Subscription para system administration", null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_userSubscription_SubscriptionId",
                table: "userSubscription",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_userSubscription_UserId",
                table: "userSubscription",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userSubscription");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
