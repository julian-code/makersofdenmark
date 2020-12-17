﻿// <auto-generated />
using System;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    [DbContext(typeof(MODContext))]
    [Migration("20201217141004_user")]
    partial class user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("CategoryTool", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ToolsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ToolsId");

                    b.HasIndex("ToolsId");

                    b.ToTable("CategoryTool");
                });

            modelBuilder.Entity("MakerSpaceTool", b =>
                {
                    b.Property<Guid>("MakerSpacesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ToolsId")
                        .HasColumnType("int");

                    b.HasKey("MakerSpacesId", "ToolsId");

                    b.HasIndex("ToolsId");

                    b.ToTable("MakerSpaceTool");
                });

            modelBuilder.Entity("MakerSpaceUser", b =>
                {
                    b.Property<Guid>("MakerSpacesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MakerSpacesId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("MakerSpaceHasMembers");
                });

            modelBuilder.Entity("MakerSpaceUser1", b =>
                {
                    b.Property<Guid>("AdminOnId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdminOnId", "AdminsId");

                    b.HasIndex("AdminsId");

                    b.ToTable("MakerSpaceHasAdmins");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Badge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MakerSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MakerSpaceId");

                    b.HasIndex("UserId");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Badge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MakerSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MakerSpaceId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.MakerSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessType")
                        .HasColumnType("int");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("AssociatedSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContactInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VATNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkShopType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactInfoId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("MakerSpace");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tool");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryTool", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MakersOfDenmark.Domain.Models.Tool", null)
                        .WithMany()
                        .HasForeignKey("ToolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MakerSpaceTool", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.MakerSpace", null)
                        .WithMany()
                        .HasForeignKey("MakerSpacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MakersOfDenmark.Domain.Models.Tool", null)
                        .WithMany()
                        .HasForeignKey("ToolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MakerSpaceUser", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.MakerSpace", null)
                        .WithMany()
                        .HasForeignKey("MakerSpacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MakersOfDenmark.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MakerSpaceUser1", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.MakerSpace", null)
                        .WithMany()
                        .HasForeignKey("AdminOnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MakersOfDenmark.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("AdminsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Badge", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.MakerSpace", null)
                        .WithMany("Badges")
                        .HasForeignKey("MakerSpaceId");

                    b.HasOne("MakersOfDenmark.Domain.Models.User", null)
                        .WithMany("Badges")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Event", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.MakerSpace", null)
                        .WithMany("Events")
                        .HasForeignKey("MakerSpaceId");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.MakerSpace", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("MakersOfDenmark.Domain.Models.ContactInfo", "ContactInfo")
                        .WithMany()
                        .HasForeignKey("ContactInfoId");

                    b.HasOne("MakersOfDenmark.Domain.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.Navigation("Address");

                    b.Navigation("ContactInfo");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Organization", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.User", b =>
                {
                    b.HasOne("MakersOfDenmark.Domain.Models.Event", null)
                        .WithMany("Participants")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.Event", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.MakerSpace", b =>
                {
                    b.Navigation("Badges");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("MakersOfDenmark.Domain.Models.User", b =>
                {
                    b.Navigation("Badges");
                });
#pragma warning restore 612, 618
        }
    }
}
