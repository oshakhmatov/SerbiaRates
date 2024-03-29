﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SerbiaRates.Data;

#nullable disable

namespace SerbiaRates.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221211213941_ExchangeRatesCreateDate")]
    partial class ExchangeRatesCreateDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SerbiaRates.Models.AverageRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("Dollar")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Euro")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("AverageRates");
                });

            modelBuilder.Entity("SerbiaRates.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("SerbiaRates.Models.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("DollarBuy")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DollarSell")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EuroBuy")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EuroSell")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("SerbiaRates.Models.ExchangeRate", b =>
                {
                    b.HasOne("SerbiaRates.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
