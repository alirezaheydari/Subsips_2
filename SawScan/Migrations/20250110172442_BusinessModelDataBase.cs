using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawScan.Migrations
{
    /// <inheritdoc />
    public partial class BusinessModelDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCooperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCooperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentSMSMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentSMSMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branche",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TelegramLink = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    XLink = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branche", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branche_BusinessCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BusinessCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Branche_BusinessCooperation_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "BusinessCooperation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DefaultMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrancheId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageContext = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaultMessage_Branche_BrancheId",
                        column: x => x.BrancheId,
                        principalTable: "Branche",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExclusiveCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrancheId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusiveCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExclusiveCustomer_Branche_BrancheId",
                        column: x => x.BrancheId,
                        principalTable: "Branche",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branche_BusinessId",
                table: "Branche",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Branche_CategoryId",
                table: "Branche",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultMessage_BrancheId",
                table: "DefaultMessage",
                column: "BrancheId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExclusiveCustomer_BrancheId",
                table: "ExclusiveCustomer",
                column: "BrancheId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultMessage");

            migrationBuilder.DropTable(
                name: "ExclusiveCustomer");

            migrationBuilder.DropTable(
                name: "SentSMSMessage");

            migrationBuilder.DropTable(
                name: "Branche");

            migrationBuilder.DropTable(
                name: "BusinessCategory");

            migrationBuilder.DropTable(
                name: "BusinessCooperation");
        }
    }
}
