using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend.Models.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    username = table.Column<string>(type: "longtext", nullable: false),
                    login = table.Column<string>(type: "longtext", nullable: false),
                    passwordHash = table.Column<string>(type: "longtext", nullable: false),
                    idPartner = table.Column<Guid>(type: "char(36)", nullable: true),
                    startBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_users_idPartner",
                        column: x => x.idPartner,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    idUser = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    operationType = table.Column<int>(type: "int", nullable: false),
                    idParent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "financial_operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    idUserAdd = table.Column<Guid>(type: "char(36)", nullable: false),
                    idCategory = table.Column<int>(type: "int", nullable: false),
                    operationType = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_financial_operations_categories_idCategory",
                        column: x => x.idCategory,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_financial_operations_users_idUserAdd",
                        column: x => x.idUserAdd,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_categories_idUser",
                table: "categories",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_financial_operations_idCategory",
                table: "financial_operations",
                column: "idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_financial_operations_idUserAdd",
                table: "financial_operations",
                column: "idUserAdd");

            migrationBuilder.CreateIndex(
                name: "IX_users_idPartner",
                table: "users",
                column: "idPartner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_operations");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
