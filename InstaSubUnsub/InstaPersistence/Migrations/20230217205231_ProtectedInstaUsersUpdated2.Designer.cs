﻿// <auto-generated />
using System;
using InstaPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InstaPersistence.Migrations
{
    [DbContext(typeof(InstaDbContext))]
    [Migration("20230217205231_ProtectedInstaUsersUpdated2")]
    partial class ProtectedInstaUsersUpdated2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InstaDomain.InstaUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Followers")
                        .HasColumnType("integer");

                    b.Property<int>("Following")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FollowingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsFollower")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UnfollowingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Name"), new[] { "IsFollower", "Status" });

                    b.ToTable("InstaUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "hidethetrack123",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 2L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "doctor_lilith",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 3L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "dr.imiller",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 4L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "mynameiswhm",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 5L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "err_yep",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 6L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "temapunk",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 7L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "sergesoukonnov",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 8L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "panther_amanita",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 9L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "igor.gord",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 10L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "olga.mikholenko",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 11L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "iriska_sia",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 12L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "oli4kakisskiss",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 13L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "err_please",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 14L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "blefamer",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 15L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "lodkaissad",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 16L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "anastasiya_kun",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 17L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "anna.saulenko",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 18L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "prikhodko5139",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 19L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "saulenkosvetlana",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 20L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "ira_knows_best",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 21L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "meltali_handmade",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 22L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "dr.ksusha_pro_edu",
                            Rank = 0,
                            Status = 3
                        },
                        new
                        {
                            Id = 23L,
                            Followers = 0,
                            Following = 0,
                            IsFollower = true,
                            Name = "lydok87",
                            Rank = 0,
                            Status = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
