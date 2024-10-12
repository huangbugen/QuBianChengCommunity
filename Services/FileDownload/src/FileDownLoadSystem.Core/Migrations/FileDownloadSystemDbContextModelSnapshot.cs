﻿// <auto-generated />
using System;
using FileDownLoadSystem.Core.EfDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileDownLoadSystem.Core.Migrations
{
    [DbContext(typeof(FileDownloadSystemDbContext))]
    partial class FileDownloadSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.FilePackages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<int?>("FilesId")
                        .HasColumnType("int");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PackageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FilesId");

                    b.ToTable("FilePackages");
                });

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.Files", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("ClickTimes")
                        .HasColumnType("bigint");

                    b.Property<long>("DownloadTimes")
                        .HasColumnType("bigint");

                    b.Property<string>("FileIconUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FileTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Notification")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.FileTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FileTypes");
                });

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.FileWebConfigs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("FileWebConfigs");
                });

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.FilePackages", b =>
                {
                    b.HasOne("FileDownLoadSystem.Entity.FileInfo.Files", null)
                        .WithMany("filePackages")
                        .HasForeignKey("FilesId");
                });

            modelBuilder.Entity("FileDownLoadSystem.Entity.FileInfo.Files", b =>
                {
                    b.Navigation("filePackages");
                });
#pragma warning restore 612, 618
        }
    }
}
