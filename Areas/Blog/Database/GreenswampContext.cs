﻿using greenswamp.Areas.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Database
{
    public partial class GreenswampContext : DbContext
    {
        public GreenswampContext()
        {
        }

        public GreenswampContext(DbContextOptions<GreenswampContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Auth> Auths { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Interaction> Interactions { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<TrendingPond> TrendingPonds { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auth>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("auth");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");
                entity.Property(e => e.LastLogin)
                    .HasColumnType("timestamp")
                    .HasColumnName("last_login");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.ResetToken).HasColumnName("reset_token");
                entity.Property(e => e.TokenExpiry)
                    .HasColumnType("timestamp")
                    .HasColumnName("token_expiry");

                entity.HasOne(d => d.User).WithOne(p => p.Auth).HasForeignKey<Auth>(d => d.UserId);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.HasIndex(e => e.PostId, "IX_events_post_id").IsUnique();

                entity.HasIndex(e => e.EventTime, "idx_events_time");

                entity.Property(e => e.EventId).HasColumnName("event_id");
                entity.Property(e => e.EventTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("event_time");
                entity.Property(e => e.HostOrg).HasColumnName("host_org");
                entity.Property(e => e.Location).HasColumnName("location");
                entity.Property(e => e.MaxCapacity).HasColumnName("max_capacity");
                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.RsvpCount)
                    .HasDefaultValueSql("0")
                    .HasColumnName("rsvp_count");

                entity.HasOne(d => d.Post).WithOne(p => p.Event)
                    .HasForeignKey<Event>(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Interaction>(entity =>
            {
                entity.ToTable("interactions");

                entity.HasIndex(e => new { e.UserId, e.PostId, e.InteractionType }, "IX_interactions_user_id_post_id_interaction_type").IsUnique();

                entity.HasIndex(e => e.InteractionType, "idx_interactions_type");

                entity.Property(e => e.InteractionId).HasColumnName("interaction_id");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");
                entity.Property(e => e.InteractionType)
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (InteractionType)Enum.Parse(typeof(InteractionType), v, true))
                    .HasColumnName("interaction_type");
                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post).WithMany(p => p.Interactions).HasForeignKey(d => d.PostId);

                entity.HasOne(d => d.User).WithMany(p => p.Interactions).HasForeignKey(d => d.UserId);
            });

            _ = modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.HasIndex(e => e.CreatedAt, "idx_posts_created");

                entity.HasIndex(e => e.PostType, "idx_posts_type");

                entity.Property(e => e.PostId).HasColumnName("post_id");
                entity.Property(e => e.AltText).HasColumnName("alt_text");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");
                entity.Property(e => e.PostType)
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (PostType)Enum.Parse(typeof(PostType), v, true))
                    .HasColumnName("post_type");
                entity.Property(e => e.MediaType)
                  .HasConversion(
                      v => v == null ? null : v.ToString().ToLower(),
                      v => v == null ? null : (MediaType?)Enum.Parse(typeof(MediaType), v, true))
                  .HasColumnName("media_type");
                entity.Property(e => e.MediaUrl).HasColumnName("media_url");
                entity.Property(e => e.ParentPostId).HasColumnName("parent_post_id");
                entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ParentPost).WithMany(p => p.InverseParentPost).HasForeignKey(d => d.ParentPostId);

                entity.HasOne(d => d.User).WithMany(p => p.Posts).HasForeignKey(d => d.UserId);

                entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                    .UsingEntity<Dictionary<string, object>>(
                        "PostTag",
                        r => r.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostId"),
                        j =>
                        {
                            j.HasKey("PostId", "TagId");
                            j.ToTable("post_tags");
                            j.IndexerProperty<long>("PostId").HasColumnName("post_id");
                            j.IndexerProperty<long>("TagId").HasColumnName("tag_id");
                        });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.HasIndex(e => e.TagName, "IX_tags_tag_name").IsUnique();

                entity.HasIndex(e => e.TagName, "idx_tags_name");

                entity.Property(e => e.TagId).HasColumnName("tag_id");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");
                entity.Property(e => e.TagName).HasColumnName("tag_name");
                entity.Property(e => e.UsageCount)
                    .HasDefaultValueSql("0")
                    .HasColumnName("usage_count");
            });

            modelBuilder.Entity<TrendingPond>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("trending_ponds");

                entity.Property(e => e.RecentPosts).HasColumnName("recent_posts");
                entity.Property(e => e.TagId).HasColumnName("tag_id");
                entity.Property(e => e.TagName).HasColumnName("tag_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "IX_users_username").IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
                entity.Property(e => e.Bio).HasColumnName("bio");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");
                entity.Property(e => e.DisplayName).HasColumnName("display_name");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("1")
                    .HasColumnType("BOOLEAN")
                    .HasColumnName("is_active");
                entity.Property(e => e.Username).HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
