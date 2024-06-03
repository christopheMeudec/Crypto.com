﻿// <auto-generated />
using System;
using Crypto.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Crypto.Core.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240603061806_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Crypto.Core.Entities.TokenEntity", b =>
                {
                    b.Property<string>("TokenCode")
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("PurchaseValue")
                        .HasColumnType("TEXT");

                    b.HasKey("TokenCode");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Crypto.Core.Entities.TokenHistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RecordedDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("TokenCode")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("TokenCode1")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("TokenCode1");

                    b.ToTable("TokensHistory");
                });

            modelBuilder.Entity("Crypto.Core.Entities.TokenHistoryEntity", b =>
                {
                    b.HasOne("Crypto.Core.Entities.TokenEntity", "Token")
                        .WithMany("History")
                        .HasForeignKey("TokenCode1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Crypto.Core.Entities.TokenEntity", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
