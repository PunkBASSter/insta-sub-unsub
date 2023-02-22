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
    [Migration("20230222183021_Updated_InstaUser_fields_and_index")]
    partial class UpdatedInstaUserfieldsandindex
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

                    b.Property<int?>("FollowersNum")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FollowingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("FollowingsNum")
                        .HasColumnType("integer");

                    b.Property<bool?>("HasRussianText")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsFollower")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastPostDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("Rank")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UnfollowingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Name"), new[] { "IsFollower", "Status", "Rank" });

                    b.ToTable("InstaUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsFollower = true,
                            Name = "hidethetrack123",
                            Status = 3
                        },
                        new
                        {
                            Id = 2L,
                            IsFollower = true,
                            Name = "doctor_lilith",
                            Status = 3
                        },
                        new
                        {
                            Id = 3L,
                            IsFollower = true,
                            Name = "dr.imiller",
                            Status = 3
                        },
                        new
                        {
                            Id = 4L,
                            IsFollower = true,
                            Name = "mynameiswhm",
                            Status = 3
                        },
                        new
                        {
                            Id = 5L,
                            IsFollower = true,
                            Name = "err_yep",
                            Status = 3
                        },
                        new
                        {
                            Id = 6L,
                            IsFollower = true,
                            Name = "temapunk",
                            Status = 3
                        },
                        new
                        {
                            Id = 7L,
                            IsFollower = true,
                            Name = "sergesoukonnov",
                            Status = 3
                        },
                        new
                        {
                            Id = 8L,
                            IsFollower = true,
                            Name = "panther_amanita",
                            Status = 3
                        },
                        new
                        {
                            Id = 9L,
                            IsFollower = true,
                            Name = "igor.gord",
                            Status = 3
                        },
                        new
                        {
                            Id = 10L,
                            IsFollower = true,
                            Name = "olga.mikholenko",
                            Status = 3
                        },
                        new
                        {
                            Id = 11L,
                            IsFollower = true,
                            Name = "iriska_sia",
                            Status = 3
                        },
                        new
                        {
                            Id = 12L,
                            IsFollower = true,
                            Name = "oli4kakisskiss",
                            Status = 3
                        },
                        new
                        {
                            Id = 13L,
                            IsFollower = true,
                            Name = "err_please",
                            Status = 3
                        },
                        new
                        {
                            Id = 14L,
                            IsFollower = true,
                            Name = "blefamer",
                            Status = 3
                        },
                        new
                        {
                            Id = 15L,
                            IsFollower = true,
                            Name = "lodkaissad",
                            Status = 3
                        },
                        new
                        {
                            Id = 16L,
                            IsFollower = true,
                            Name = "anastasiya_kun",
                            Status = 3
                        },
                        new
                        {
                            Id = 17L,
                            IsFollower = true,
                            Name = "anna.saulenko",
                            Status = 3
                        },
                        new
                        {
                            Id = 18L,
                            IsFollower = true,
                            Name = "prikhodko5139",
                            Status = 3
                        },
                        new
                        {
                            Id = 19L,
                            IsFollower = true,
                            Name = "saulenkosvetlana",
                            Status = 3
                        },
                        new
                        {
                            Id = 20L,
                            IsFollower = true,
                            Name = "ira_knows_best",
                            Status = 3
                        },
                        new
                        {
                            Id = 21L,
                            IsFollower = true,
                            Name = "meltali_handmade",
                            Status = 3
                        },
                        new
                        {
                            Id = 22L,
                            IsFollower = true,
                            Name = "dr.ksusha_pro_edu",
                            Status = 3
                        },
                        new
                        {
                            Id = 23L,
                            IsFollower = true,
                            Name = "lydok87",
                            Status = 3
                        });
                });

            modelBuilder.Entity("InstaDomain.UserRelation", b =>
                {
                    b.Property<long>("FollowerId")
                        .HasColumnType("bigint");

                    b.Property<long>("FolloweeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FollowerId", "FolloweeId");

                    b.HasIndex("FolloweeId");

                    b.ToTable("UserRelations");
                });

            modelBuilder.Entity("InstaDomain.UserRelation", b =>
                {
                    b.HasOne("InstaDomain.InstaUser", "Followee")
                        .WithMany("Followees")
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InstaDomain.InstaUser", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followee");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("InstaDomain.InstaUser", b =>
                {
                    b.Navigation("Followees");

                    b.Navigation("Followers");
                });
#pragma warning restore 612, 618
        }
    }
}
