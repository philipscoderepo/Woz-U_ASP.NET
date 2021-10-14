﻿// <auto-generated />
using Lesson6_HandsOn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lesson6_HandsOn.Migrations
{
    [DbContext(typeof(AlienContext))]
    partial class AlienContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Lesson6_HandsOn.Models.Alien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumArms")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumHeads")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumLegs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlanetOfOrigin")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Alien");
                });
#pragma warning restore 612, 618
        }
    }
}
