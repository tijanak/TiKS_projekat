﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.Donacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int?>("SlucajID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("SlucajID");

                    b.ToTable("Donacija");
                });

            modelBuilder.Entity("Backend.Models.Kategorija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Tip")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Kategorija");
                });

            modelBuilder.Entity("Backend.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Backend.Models.Lokacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Lokacija");
                });

            modelBuilder.Entity("Backend.Models.Novost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SlucajID")
                        .HasColumnType("int");

                    b.Property<string>("Tekst")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SlucajID");

                    b.ToTable("Novost");
                });

            modelBuilder.Entity("Backend.Models.Slucaj", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("LokacijaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Opis")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slika")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ZivotinjaId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("LokacijaId")
                        .IsUnique();

                    b.HasIndex("ZivotinjaId")
                        .IsUnique();

                    b.ToTable("Slucaj");
                });

            modelBuilder.Entity("Backend.Models.Trosak", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<string>("Namena")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("SlucajID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SlucajID");

                    b.ToTable("Trosak");
                });

            modelBuilder.Entity("Backend.Models.Zivotinja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Ime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Vrsta")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Zivotinja");
                });

            modelBuilder.Entity("KategorijaSlucaj", b =>
                {
                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<int>("SlucajeviID")
                        .HasColumnType("int");

                    b.HasKey("KategorijaID", "SlucajeviID");

                    b.HasIndex("SlucajeviID");

                    b.ToTable("KategorijaSlucaj");
                });

            modelBuilder.Entity("Backend.Models.Donacija", b =>
                {
                    b.HasOne("Backend.Models.Korisnik", "Korisnik")
                        .WithMany("Donacije")
                        .HasForeignKey("KorisnikID");

                    b.HasOne("Backend.Models.Slucaj", "Slucaj")
                        .WithMany("Donacije")
                        .HasForeignKey("SlucajID");

                    b.Navigation("Korisnik");

                    b.Navigation("Slucaj");
                });

            modelBuilder.Entity("Backend.Models.Novost", b =>
                {
                    b.HasOne("Backend.Models.Slucaj", "Slucaj")
                        .WithMany("Novosti")
                        .HasForeignKey("SlucajID");

                    b.Navigation("Slucaj");
                });

            modelBuilder.Entity("Backend.Models.Slucaj", b =>
                {
                    b.HasOne("Backend.Models.Korisnik", "Korisnik")
                        .WithMany("Slucajevi")
                        .HasForeignKey("KorisnikID");

                    b.HasOne("Backend.Models.Lokacija", "Lokacija")
                        .WithOne("Slucaj")
                        .HasForeignKey("Backend.Models.Slucaj", "LokacijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.Zivotinja", "Zivotinja")
                        .WithOne("Slucaj")
                        .HasForeignKey("Backend.Models.Slucaj", "ZivotinjaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Lokacija");

                    b.Navigation("Zivotinja");
                });

            modelBuilder.Entity("Backend.Models.Trosak", b =>
                {
                    b.HasOne("Backend.Models.Slucaj", "Slucaj")
                        .WithMany("Troskovi")
                        .HasForeignKey("SlucajID");

                    b.Navigation("Slucaj");
                });

            modelBuilder.Entity("KategorijaSlucaj", b =>
                {
                    b.HasOne("Backend.Models.Kategorija", null)
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.Slucaj", null)
                        .WithMany()
                        .HasForeignKey("SlucajeviID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Models.Korisnik", b =>
                {
                    b.Navigation("Donacije");

                    b.Navigation("Slucajevi");
                });

            modelBuilder.Entity("Backend.Models.Lokacija", b =>
                {
                    b.Navigation("Slucaj")
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Models.Slucaj", b =>
                {
                    b.Navigation("Donacije");

                    b.Navigation("Novosti");

                    b.Navigation("Troskovi");
                });

            modelBuilder.Entity("Backend.Models.Zivotinja", b =>
                {
                    b.Navigation("Slucaj")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
