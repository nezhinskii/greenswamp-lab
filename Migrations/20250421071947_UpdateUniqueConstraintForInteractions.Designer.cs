﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using greenswamp.Database;

#nullable disable

namespace greenswamp.Migrations
{
    [DbContext(typeof(GreenswampContext))]
    [Migration("20250421071947_UpdateUniqueConstraintForInteractions")]
    partial class UpdateUniqueConstraintForInteractions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("claim_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.ToTable("role_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("claim_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("user_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("user_logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<long>("PostId")
                        .HasColumnType("bigint")
                        .HasColumnName("post_id");

                    b.Property<long>("TagId")
                        .HasColumnType("bigint")
                        .HasColumnName("tag_id");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("post_tags", (string)null);
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Event", b =>
                {
                    b.Property<long>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("event_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("EventId"));

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("timestamp")
                        .HasColumnName("event_time");

                    b.Property<string>("HostOrg")
                        .HasColumnType("text")
                        .HasColumnName("host_org");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<long?>("MaxCapacity")
                        .HasColumnType("bigint")
                        .HasColumnName("max_capacity");

                    b.Property<long?>("PostId")
                        .HasColumnType("bigint")
                        .HasColumnName("post_id");

                    b.Property<long?>("RsvpCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("rsvp_count")
                        .HasDefaultValueSql("0");

                    b.HasKey("EventId");

                    b.HasIndex(new[] { "PostId" }, "IX_events_post_id")
                        .IsUnique();

                    b.HasIndex(new[] { "EventTime" }, "idx_events_time");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Interaction", b =>
                {
                    b.Property<long>("InteractionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("interaction_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("InteractionId"));

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("InteractionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("interaction_type");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint")
                        .HasColumnName("post_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("InteractionId");

                    b.HasIndex("PostId");

                    b.HasIndex(new[] { "UserId", "PostId", "InteractionType" }, "IX_interactions_user_id_post_id_interaction_type")
                        .IsUnique();

                    b.HasIndex(new[] { "InteractionType" }, "idx_interactions_type");

                    b.ToTable("interactions", (string)null);
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Post", b =>
                {
                    b.Property<long>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("post_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("PostId"));

                    b.Property<string>("AltText")
                        .HasColumnType("text")
                        .HasColumnName("alt_text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("MediaType")
                        .HasColumnType("text")
                        .HasColumnName("media_type");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("text")
                        .HasColumnName("media_url");

                    b.Property<long?>("ParentPostId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_post_id");

                    b.Property<string>("PostType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("post_type");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("text")
                        .HasColumnName("thumbnail_url");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("PostId");

                    b.HasIndex("ParentPostId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "CreatedAt" }, "idx_posts_created");

                    b.HasIndex(new[] { "PostType" }, "idx_posts_type");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Tag", b =>
                {
                    b.Property<long>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("tag_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TagId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tag_name");

                    b.Property<long?>("UsageCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("usage_count")
                        .HasDefaultValueSql("0");

                    b.HasKey("TagId");

                    b.HasIndex(new[] { "TagName" }, "IX_tags_tag_name")
                        .IsUnique();

                    b.HasIndex(new[] { "TagName" }, "idx_tags_name");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.TrendingPond", b =>
                {
                    b.Property<long?>("RecentPosts")
                        .HasColumnType("bigint")
                        .HasColumnName("recent_posts");

                    b.Property<long?>("TagId")
                        .HasColumnType("bigint")
                        .HasColumnName("tag_id");

                    b.Property<string>("TagName")
                        .HasColumnType("text")
                        .HasColumnName("tag_name");

                    b.ToTable((string)null);

                    b.ToView("trending_ponds", (string)null);
                });

            modelBuilder.Entity("greenswamp.Models.Auth", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("auth", (string)null);
                });

            modelBuilder.Entity("greenswamp.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text")
                        .HasColumnName("avatar_url");

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("is_active")
                        .HasDefaultValueSql("true");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.HasIndex(new[] { "Username" }, "IX_users_username")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("greenswamp.Areas.Blog.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("greenswamp.Areas.Blog.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Event", b =>
                {
                    b.HasOne("greenswamp.Areas.Blog.Models.Post", "Post")
                        .WithOne("Event")
                        .HasForeignKey("greenswamp.Areas.Blog.Models.Event", "PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Interaction", b =>
                {
                    b.HasOne("greenswamp.Areas.Blog.Models.Post", "Post")
                        .WithMany("Interactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("greenswamp.Models.User", "User")
                        .WithMany("Interactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Post", b =>
                {
                    b.HasOne("greenswamp.Areas.Blog.Models.Post", "ParentPost")
                        .WithMany("InverseParentPost")
                        .HasForeignKey("ParentPostId");

                    b.HasOne("greenswamp.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentPost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("greenswamp.Models.User", b =>
                {
                    b.HasOne("greenswamp.Models.Auth", "Auth")
                        .WithOne("User")
                        .HasForeignKey("greenswamp.Models.User", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("greenswamp.Areas.Blog.Models.Post", b =>
                {
                    b.Navigation("Event");

                    b.Navigation("Interactions");

                    b.Navigation("InverseParentPost");
                });

            modelBuilder.Entity("greenswamp.Models.Auth", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("greenswamp.Models.User", b =>
                {
                    b.Navigation("Interactions");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
