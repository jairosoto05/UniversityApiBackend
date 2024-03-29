﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using UniversityApiBackend.DataAccess;

#nullable disable

namespace UniversityApiBackend.Migrations
{
    [DbContext(typeof(UniversityDBContext))]
    partial class UniversityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("CATEGORIES");
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("Level")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("NVARCHAR2(280)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("COURSES");
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("STUDENTS");
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Rol")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("UniversityApiBackend.Models.DataModels.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityApiBackend.Models.DataModels.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.Course", b =>
                {
                    b.HasOne("UniversityApiBackend.Models.DataModels.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("UniversityApiBackend.Models.DataModels.Category", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
