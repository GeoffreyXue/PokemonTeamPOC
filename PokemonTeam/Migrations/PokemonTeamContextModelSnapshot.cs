﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonTeam.Data;

#nullable disable

namespace PokemonTeam.Migrations
{
    [DbContext(typeof(PokemonTeamContext))]
    partial class PokemonTeamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PokemonTeam.Models.Pokemon", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.HasIndex("TeamID");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("PokemonTeam.Models.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Team", (string)null);
                });

            modelBuilder.Entity("PokemonTeam.Models.Pokemon", b =>
                {
                    b.HasOne("PokemonTeam.Models.Team", null)
                        .WithMany("Roster")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("PokemonTeam.Models.Team", b =>
                {
                    b.Navigation("Roster");
                });
#pragma warning restore 612, 618
        }
    }
}