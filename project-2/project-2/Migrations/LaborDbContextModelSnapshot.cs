﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace project_2.Migrations
{
    [DbContext(typeof(LaborDbContext))]
    partial class LaborDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.cAkkrMintavetel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AkkrMintavetelStatusz")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AkkrMintavetelStatusz")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("AkkrMintavetel");
                });

            modelBuilder.Entity("Domain.cEredmeny", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<float?>("AlsoMh")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ertek")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<float?>("ErtekHozzarendelt")
                        .HasColumnType("real");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<float?>("MaxRange")
                        .HasColumnType("real");

                    b.Property<string>("Megyseg")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<long>("MintaId")
                        .HasColumnType("bigint");

                    b.Property<string>("ParKod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Megyseg");

                    b.HasIndex("MintaId");

                    b.HasIndex("ParKod");

                    b.ToTable("Eredmeny");
                });

            modelBuilder.Entity("Domain.cHUMVIfelelos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Cim")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Felelos")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Felelos")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("HumviFelelos");
                });

            modelBuilder.Entity("Domain.cHUMVImodul", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ModulKod")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("HumviModul");
                });

            modelBuilder.Entity("Domain.cMertekegyseg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("HumviLeiras")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Megyseg")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SajatLeiras")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Mertekegyseg");
                });

            modelBuilder.Entity("Domain.cMinta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AkkrMintavetel")
                        .HasMaxLength(10)
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<long>("Felelos")
                        .HasMaxLength(10)
                        .HasColumnType("bigint");

                    b.Property<bool>("HUMVIexport")
                        .HasColumnType("bit");

                    b.Property<string>("LabAkkrSzam")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Labor")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LaborMintaKod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MintaAtvetel")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mintavevo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ModulKod")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MvAkkrSzam")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("MvDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("MvHely")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MvOk")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MvOkaEgyeb")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MvTipus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MvhKod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("VizsgalatKezdete")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VizsgalatVege")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AkkrMintavetel");

                    b.HasIndex("Felelos");

                    b.HasIndex("Id");

                    b.HasIndex("ModulKod");

                    b.HasIndex("MvOk");

                    b.HasIndex("MvTipus");

                    b.HasIndex("MvhKod");

                    b.HasIndex("Labor", "LabAkkrSzam");

                    b.HasIndex("Mintavevo", "MvAkkrSzam");

                    b.ToTable("Minta");
                });

            modelBuilder.Entity("Domain.cMintavevo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Cim")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ErvKezdete")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ErvVege")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("MintavevoAzonosito")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MvAkkrSzam")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Mintavevo");
                });

            modelBuilder.Entity("Domain.cMvHely", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<float?>("GPS_E_X")
                        .HasColumnType("real");

                    b.Property<float?>("GPS_N_Y")
                        .HasColumnType("real");

                    b.Property<string>("HumviRegiNev")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("MvhKod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("NevRovid")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("NevSajat")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("NevTeljes")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Telepules")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Tipus")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("VizBazis")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("MvHely");
                });

            modelBuilder.Entity("Domain.cMvOka", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("MvOk")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("MvOka");
                });

            modelBuilder.Entity("Domain.cMvTipus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MvTipusNev")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("MvTipus");
                });

            modelBuilder.Entity("Domain.cParameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("HumviLeiras")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParKod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ParamErtek")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("ParamTip")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("SajatLeiras")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("Domain.cVizsgaloLabor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Cim")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ErvKezdete")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ErvVege")
                        .HasColumnType("datetime2");

                    b.Property<string>("LabAkkrSzam")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Labor")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("VizsgaloLabor");
                });

            modelBuilder.Entity("Domain.cEredmeny", b =>
                {
                    b.HasOne("Domain.cMertekegyseg", "Mertekegyseg")
                        .WithMany("Eredmeny")
                        .HasForeignKey("Megyseg")
                        .HasPrincipalKey("Megyseg")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cMinta", "Minta")
                        .WithMany()
                        .HasForeignKey("MintaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.cParameter", "Parameter")
                        .WithMany("Eredmeny")
                        .HasForeignKey("ParKod")
                        .HasPrincipalKey("ParKod")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Mertekegyseg");

                    b.Navigation("Minta");

                    b.Navigation("Parameter");
                });

            modelBuilder.Entity("Domain.cMinta", b =>
                {
                    b.HasOne("Domain.cAkkrMintavetel", "cAkkrMintavetel")
                        .WithMany("Minta")
                        .HasForeignKey("AkkrMintavetel")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.cHUMVIfelelos", "cHUMVIfelelos")
                        .WithMany("Minta")
                        .HasForeignKey("Felelos")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cHUMVImodul", "cHUMVImodul")
                        .WithMany("Minta")
                        .HasForeignKey("ModulKod")
                        .HasPrincipalKey("ModulKod")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cMvOka", "cMvOka")
                        .WithMany("Minta")
                        .HasForeignKey("MvOk")
                        .HasPrincipalKey("MvOk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cMvTipus", "cMvTipus")
                        .WithMany("Minta")
                        .HasForeignKey("MvTipus")
                        .HasPrincipalKey("MvTipusNev")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cMvHely", "cMvHely")
                        .WithMany("Minta")
                        .HasForeignKey("MvhKod")
                        .HasPrincipalKey("MvhKod")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.cVizsgaloLabor", "cVizsgaloLabor")
                        .WithMany("Minta")
                        .HasForeignKey("Labor", "LabAkkrSzam")
                        .HasPrincipalKey("Labor", "LabAkkrSzam")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.cMintavevo", "cMintavevo")
                        .WithMany("Minta")
                        .HasForeignKey("Mintavevo", "MvAkkrSzam")
                        .HasPrincipalKey("MintavevoAzonosito", "MvAkkrSzam")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("cAkkrMintavetel");

                    b.Navigation("cHUMVIfelelos");

                    b.Navigation("cHUMVImodul");

                    b.Navigation("cMintavevo");

                    b.Navigation("cMvHely");

                    b.Navigation("cMvOka");

                    b.Navigation("cMvTipus");

                    b.Navigation("cVizsgaloLabor");
                });

            modelBuilder.Entity("Domain.cAkkrMintavetel", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cHUMVIfelelos", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cHUMVImodul", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cMertekegyseg", b =>
                {
                    b.Navigation("Eredmeny");
                });

            modelBuilder.Entity("Domain.cMintavevo", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cMvHely", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cMvOka", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cMvTipus", b =>
                {
                    b.Navigation("Minta");
                });

            modelBuilder.Entity("Domain.cParameter", b =>
                {
                    b.Navigation("Eredmeny");
                });

            modelBuilder.Entity("Domain.cVizsgaloLabor", b =>
                {
                    b.Navigation("Minta");
                });
#pragma warning restore 612, 618
        }
    }
}
