﻿// <auto-generated />
using System;
using CatsAndPies.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241002200330_added Questionnairies")]
    partial class addedQuestionnairies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CatsAndPies.Domain.Entities.QuestionnaireEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("ChillTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Dish")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Dream")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Film")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Flower")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Hobby")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PositiveTraits")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Season")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Singer")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Questionnaire", (string)null);
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.QuestionnaireEntity", b =>
                {
                    b.HasOne("CatsAndPies.Domain.Entities.UserEntity", "User")
                        .WithOne("Questionnaire")
                        .HasForeignKey("CatsAndPies.Domain.Entities.QuestionnaireEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("CatsAndPies.Domain.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CatsAndPies.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Questionnaire");
                });
#pragma warning restore 612, 618
        }
    }
}
