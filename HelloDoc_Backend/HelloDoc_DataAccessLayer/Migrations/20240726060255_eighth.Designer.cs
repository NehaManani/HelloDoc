﻿// <auto-generated />
using System;
using HelloDoc_DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HelloDoc_DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240726060255_eighth")]
    partial class eighth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HelloDoc_Entities.DataModels.BloodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BloodGroup")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.HasKey("Id");

                    b.ToTable("BloodTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BloodGroup = "A+"
                        },
                        new
                        {
                            Id = 2,
                            BloodGroup = "A-"
                        },
                        new
                        {
                            Id = 3,
                            BloodGroup = "B+"
                        },
                        new
                        {
                            Id = 4,
                            BloodGroup = "B-"
                        },
                        new
                        {
                            Id = 5,
                            BloodGroup = "AB+"
                        },
                        new
                        {
                            Id = 6,
                            BloodGroup = "AB-"
                        },
                        new
                        {
                            Id = 7,
                            BloodGroup = "O+"
                        },
                        new
                        {
                            Id = 8,
                            BloodGroup = "O-"
                        });
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Male"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Female"
                        });
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.PatientDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Allergies")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int?>("BloodTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("ConfirmationNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentMedications")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Document")
                        .HasColumnType("text");

                    b.Property<string>("EmergencyContactName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("EmergencyContactNumber")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("MedicalHistory")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BloodTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("PatientDetails");
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.ProviderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminNotes")
                        .HasColumnType("text");

                    b.Property<bool>("BackgroundCheck")
                        .HasColumnType("boolean");

                    b.Property<string>("BackgroundCheckDocument")
                        .HasColumnType("text");

                    b.Property<string>("BusinessName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("BusinessWebsite")
                        .HasColumnType("text");

                    b.Property<string>("ConfirmationNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("ContractorAgreement")
                        .HasColumnType("boolean");

                    b.Property<string>("ContractorDocument")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Document")
                        .HasColumnType("text");

                    b.Property<bool>("HipaaCompliance")
                        .HasColumnType("boolean");

                    b.Property<string>("HipaaComplianceDocument")
                        .HasColumnType("text");

                    b.Property<string>("MedicalLicense")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("NonDisclosureAgreement")
                        .HasColumnType("boolean");

                    b.Property<string>("NonDisclosureDocument")
                        .HasColumnType("text");

                    b.Property<string>("NpiNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ProviderDetails");
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("OTP")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<DateTime?>("OtpExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("ReasonForBlock")
                        .HasColumnType("text");

                    b.Property<byte>("Role")
                        .HasMaxLength(16)
                        .HasColumnType("smallint");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Zip")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("Gender");

                    b.HasIndex("Role");

                    b.HasIndex("Status");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.UserRole", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("smallint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Role = "Admin"
                        },
                        new
                        {
                            Id = (byte)2,
                            Role = "Patient"
                        },
                        new
                        {
                            Id = (byte)3,
                            Role = "Provider"
                        });
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.UserStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("smallint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Status = "New"
                        },
                        new
                        {
                            Id = (byte)2,
                            Status = "Pending"
                        },
                        new
                        {
                            Id = (byte)3,
                            Status = "Active"
                        },
                        new
                        {
                            Id = (byte)4,
                            Status = "Conclude"
                        },
                        new
                        {
                            Id = (byte)5,
                            Status = "Close"
                        },
                        new
                        {
                            Id = (byte)6,
                            Status = "Unpaid"
                        });
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.PatientDetails", b =>
                {
                    b.HasOne("HelloDoc_Entities.DataModels.BloodType", "BloodType")
                        .WithMany()
                        .HasForeignKey("BloodTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HelloDoc_Entities.DataModels.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BloodType");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.ProviderDetails", b =>
                {
                    b.HasOne("HelloDoc_Entities.DataModels.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("HelloDoc_Entities.DataModels.User", b =>
                {
                    b.HasOne("HelloDoc_Entities.DataModels.Gender", "Genders")
                        .WithMany()
                        .HasForeignKey("Gender")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HelloDoc_Entities.DataModels.UserRole", "UserRoles")
                        .WithMany()
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HelloDoc_Entities.DataModels.UserStatus", "UserStatuses")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Genders");

                    b.Navigation("UserRoles");

                    b.Navigation("UserStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
