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
    [Migration("20230321000113_Added_KeyValueJson_storage_to_DB")]
    partial class Added_KeyValueJson_storage_to_DB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InstaCrawlerApp.JobAuditRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ErrorInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecutionStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LimitPerIteration")
                        .HasColumnType("integer");

                    b.Property<int>("ProcessedNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("JobAudit");
                });

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

                    b.Property<bool?>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsFollower")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastPostDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<double>("Rank")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UnfollowingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Name"), new[] { "IsFollower", "Status", "Rank", "LastPostDate" });

                    b.ToTable("InstaUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsFollower = true,
                            Name = "hidethetrack123",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 2L,
                            IsFollower = true,
                            Name = "doctor_lilith",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 3L,
                            IsFollower = true,
                            Name = "dr.imiller",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 4L,
                            IsFollower = true,
                            Name = "mynameiswhm",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 5L,
                            IsFollower = true,
                            Name = "err_yep",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 6L,
                            IsFollower = true,
                            Name = "temapunk",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 7L,
                            IsFollower = true,
                            Name = "sergesoukonnov",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 8L,
                            IsFollower = true,
                            Name = "panther_amanita",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 9L,
                            IsFollower = true,
                            Name = "igor.gord",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 10L,
                            IsFollower = true,
                            Name = "olga.mikholenko",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 11L,
                            IsFollower = true,
                            Name = "iriska_sia",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 12L,
                            IsFollower = true,
                            Name = "oli4kakisskiss",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 13L,
                            IsFollower = true,
                            Name = "err_please",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 14L,
                            IsFollower = true,
                            Name = "blefamer",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 15L,
                            IsFollower = true,
                            Name = "lodkaissad",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 16L,
                            IsFollower = true,
                            Name = "anastasiya_kun",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 17L,
                            IsFollower = true,
                            Name = "anna.saulenko",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 18L,
                            IsFollower = true,
                            Name = "prikhodko5139",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 19L,
                            IsFollower = true,
                            Name = "saulenkosvetlana",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 20L,
                            IsFollower = true,
                            Name = "ira_knows_best",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 21L,
                            IsFollower = true,
                            Name = "meltali_handmade",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 22L,
                            IsFollower = true,
                            Name = "dr.ksusha_pro_edu",
                            Rank = 0.0,
                            Status = 3
                        },
                        new
                        {
                            Id = 23L,
                            IsFollower = true,
                            Name = "lydok87",
                            Rank = 0.0,
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

            modelBuilder.Entity("InstaPersistence.Utils.KeyValueJson", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("jsonb");

                    b.HasKey("Key");

                    b.ToTable("KeyValueJsonObjects");
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
