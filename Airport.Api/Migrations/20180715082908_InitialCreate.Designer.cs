﻿// <auto-generated />
using System;
using Airport.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Airport.Api.Migrations
{
    [DbContext(typeof(AirportDbContext))]
    [Migration("20180715082908_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Airport.Data.Models.Airhostess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("CrewId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("CrewId");

                    b.ToTable("Airhostess");
                });

            modelBuilder.Entity("Airport.Data.Models.Crew", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PilotId");

                    b.HasKey("Id");

                    b.ToTable("Crew");
                });

            modelBuilder.Entity("Airport.Data.Models.Departure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CrewId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FlightId");

                    b.Property<int>("PlaneId");

                    b.HasKey("Id");

                    b.HasIndex("CrewId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PlaneId");

                    b.ToTable("Departure");
                });

            modelBuilder.Entity("Airport.Data.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArrivalPlace");

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<string>("DeparturePlace");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("Airport.Data.Models.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<double>("Experience");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Pilot");
                });

            modelBuilder.Entity("Airport.Data.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("PlaneTypeId");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<TimeSpan>("ServiceLife");

                    b.HasKey("Id");

                    b.HasIndex("PlaneTypeId");

                    b.ToTable("Plane");
                });

            modelBuilder.Entity("Airport.Data.Models.PlaneType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Carrying");

                    b.Property<string>("Model");

                    b.Property<int>("Seats");

                    b.HasKey("Id");

                    b.ToTable("PlaneType");
                });

            modelBuilder.Entity("Airport.Data.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlightId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("Airport.Data.Models.Airhostess", b =>
                {
                    b.HasOne("Airport.Data.Models.Crew", "Crew")
                        .WithMany("Airhostesses")
                        .HasForeignKey("CrewId");
                });

            modelBuilder.Entity("Airport.Data.Models.Departure", b =>
                {
                    b.HasOne("Airport.Data.Models.Crew", "Crew")
                        .WithMany()
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Airport.Data.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Airport.Data.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Airport.Data.Models.Plane", b =>
                {
                    b.HasOne("Airport.Data.Models.PlaneType", "PlaneType")
                        .WithMany()
                        .HasForeignKey("PlaneTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Airport.Data.Models.Ticket", b =>
                {
                    b.HasOne("Airport.Data.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
