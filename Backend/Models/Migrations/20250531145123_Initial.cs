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
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                })
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
                    startBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    idRole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_idRole",
                        column: x => x.idRole,
                        principalTable: "roles",
                        principalColumn: "id");
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
                    idParent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_categories_idParent",
                        column: x => x.idParent,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_categories_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_days_users_IdUser",
                        column: x => x.IdUser,
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
                    IdDay = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<Guid>(type: "char(36)", nullable: false),
                    idCategory = table.Column<int>(type: "int", nullable: true),
                    operationType = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_financial_operations_categories_idCategory",
                        column: x => x.idCategory,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_financial_operations_days_IdDay",
                        column: x => x.IdDay,
                        principalTable: "days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_financial_operations_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_categories_idParent",
                table: "categories",
                column: "idParent");

            migrationBuilder.CreateIndex(
                name: "IX_categories_idUser",
                table: "categories",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_days_IdUser",
                table: "days",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_financial_operations_idCategory",
                table: "financial_operations",
                column: "idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_financial_operations_IdDay",
                table: "financial_operations",
                column: "IdDay");

            migrationBuilder.CreateIndex(
                name: "IX_financial_operations_idUser",
                table: "financial_operations",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_users_idPartner",
                table: "users",
                column: "idPartner");

            migrationBuilder.CreateIndex(
                name: "IX_users_idRole",
                table: "users",
                column: "idRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_operations");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "days");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
