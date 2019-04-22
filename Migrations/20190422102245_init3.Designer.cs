﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace iot.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20190422102245_init3")]
    partial class init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceName")
                        .IsRequired();

                    b.Property<float>("Lat");

                    b.Property<float>("Long");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("DeviceLog", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeviceId");

                    b.Property<float>("Humidity");

                    b.Property<float>("Sound");

                    b.Property<float>("Temperature");

                    b.Property<DateTime>("Time");

                    b.HasKey("No");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceLogs");
                });

            modelBuilder.Entity("DeviceLog", b =>
                {
                    b.HasOne("Device", "Device")
                        .WithMany("DeviceLogs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}