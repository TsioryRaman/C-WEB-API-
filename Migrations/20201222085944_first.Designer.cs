﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tache.Entities.Contexte;

namespace Tache.Migrations
{
    [DbContext(typeof(TacheContext))]
    [Migration("20201222085944_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PersonnelsTaches", b =>
                {
                    b.Property<int>("personnelsIdPersonnel")
                        .HasColumnType("int");

                    b.Property<int>("tachesIdTache")
                        .HasColumnType("int");

                    b.HasKey("personnelsIdPersonnel", "tachesIdTache");

                    b.HasIndex("tachesIdTache");

                    b.ToTable("PersonnelsTaches");
                });

            modelBuilder.Entity("Tache.Entities.Departement.Departements", b =>
                {
                    b.Property<int>("IdDepartement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DepartementName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDepartement");

                    b.ToTable("Departements");
                });

            modelBuilder.Entity("Tache.Entities.Personnel.Personnels", b =>
                {
                    b.Property<int>("IdPersonnel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateWork")
                        .HasColumnType("Date");

                    b.Property<int>("DepartementId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fonction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImagePersonnel")
                        .HasColumnType("Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPersonnel");

                    b.HasIndex("DepartementId");

                    b.ToTable("Personnels");
                });

            modelBuilder.Entity("Tache.Entities.Tache.Taches", b =>
                {
                    b.Property<int>("IdTache")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TacheName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("Date");

                    b.HasKey("IdTache");

                    b.ToTable("Taches");
                });

            modelBuilder.Entity("Tache.Entities.User.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonnelsTaches", b =>
                {
                    b.HasOne("Tache.Entities.Personnel.Personnels", null)
                        .WithMany()
                        .HasForeignKey("personnelsIdPersonnel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tache.Entities.Tache.Taches", null)
                        .WithMany()
                        .HasForeignKey("tachesIdTache")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tache.Entities.Personnel.Personnels", b =>
                {
                    b.HasOne("Tache.Entities.Departement.Departements", "Departement")
                        .WithMany("Personnel")
                        .HasForeignKey("DepartementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departement");
                });

            modelBuilder.Entity("Tache.Entities.Departement.Departements", b =>
                {
                    b.Navigation("Personnel");
                });
#pragma warning restore 612, 618
        }
    }
}
