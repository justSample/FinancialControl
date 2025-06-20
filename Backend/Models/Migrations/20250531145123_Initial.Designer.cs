﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Models.Migrations
{
    [DbContext(typeof(FinancialDbContext))]
    [Migration("20250531145123_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend.Models.Db.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("IdParent")
                        .HasColumnType("int")
                        .HasColumnName("idParent");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("char(36)")
                        .HasColumnName("idUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("IdParent");

                    b.HasIndex("IdUser");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Backend.Models.Db.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("days");
                });

            modelBuilder.Entity("Backend.Models.Db.FinancialOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("idCategory");

                    b.Property<int>("IdDay")
                        .HasColumnType("int");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("char(36)")
                        .HasColumnName("idUser");

                    b.Property<int>("OperationType")
                        .HasColumnType("int")
                        .HasColumnName("operationType");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdDay");

                    b.HasIndex("IdUser");

                    b.ToTable("financial_operations");
                });

            modelBuilder.Entity("Backend.Models.Db.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Backend.Models.Db.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<Guid?>("IdPartner")
                        .HasColumnType("char(36)")
                        .HasColumnName("idPartner");

                    b.Property<int?>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("idRole");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("passwordHash");

                    b.Property<decimal>("StartBalance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("startBalance");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("IdPartner");

                    b.HasIndex("IdRole");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Backend.Models.Db.Category", b =>
                {
                    b.HasOne("Backend.Models.Db.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("IdParent")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Backend.Models.Db.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.Db.Day", b =>
                {
                    b.HasOne("Backend.Models.Db.User", "User")
                        .WithMany("Days")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.Db.FinancialOperation", b =>
                {
                    b.HasOne("Backend.Models.Db.Category", "Category")
                        .WithMany()
                        .HasForeignKey("IdCategory");

                    b.HasOne("Backend.Models.Db.Day", "Day")
                        .WithMany("FinancialOperations")
                        .HasForeignKey("IdDay")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.Db.User", "User")
                        .WithMany("FinancialOperations")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.Db.User", b =>
                {
                    b.HasOne("Backend.Models.Db.User", "Partner")
                        .WithMany("Partners")
                        .HasForeignKey("IdPartner")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Backend.Models.Db.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole");

                    b.Navigation("Partner");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Backend.Models.Db.Category", b =>
                {
                    b.Navigation("ChildCategories");
                });

            modelBuilder.Entity("Backend.Models.Db.Day", b =>
                {
                    b.Navigation("FinancialOperations");
                });

            modelBuilder.Entity("Backend.Models.Db.User", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Days");

                    b.Navigation("FinancialOperations");

                    b.Navigation("Partners");
                });
#pragma warning restore 612, 618
        }
    }
}
