﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SteamDeckWindows;

#nullable disable

namespace SteamDeckWindows.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("SteamDeckWindows.Models.EmulatorSetting", b =>
                {
                    b.Property<int>("EmulatorSettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Install")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ResetSettings")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SettingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmulatorSettingId");

                    b.HasIndex("SettingId");

                    b.ToTable("EmulatorSetting");
                });

            modelBuilder.Entity("SteamDeckWindows.Models.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("SteamDeckWindows.Models.ToolSetting", b =>
                {
                    b.Property<int>("ToolSettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Install")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SettingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ToolSettingId");

                    b.HasIndex("SettingId");

                    b.ToTable("ToolSetting");
                });

            modelBuilder.Entity("SteamDeckWindows.Models.EmulatorSetting", b =>
                {
                    b.HasOne("SteamDeckWindows.Models.Setting", "Setting")
                        .WithMany("Emulators")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Setting");
                });

            modelBuilder.Entity("SteamDeckWindows.Models.ToolSetting", b =>
                {
                    b.HasOne("SteamDeckWindows.Models.Setting", "Setting")
                        .WithMany("Tools")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Setting");
                });

            modelBuilder.Entity("SteamDeckWindows.Models.Setting", b =>
                {
                    b.Navigation("Emulators");

                    b.Navigation("Tools");
                });
#pragma warning restore 612, 618
        }
    }
}
