using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebSteamParser.Models
{
    public partial class u1286676_STEAM_PARSINGContext : DbContext
    {
        public u1286676_STEAM_PARSINGContext()
        {
        }

        public u1286676_STEAM_PARSINGContext(DbContextOptions<u1286676_STEAM_PARSINGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GameList> GameLists { get; set; } = null!;
        public virtual DbSet<GameListTempKz> GameListTempKzs { get; set; } = null!;
        public virtual DbSet<GameListTempRu> GameListTempRus { get; set; } = null!;
        public virtual DbSet<GameListTempTr> GameListTempTrs { get; set; } = null!;
        public virtual DbSet<GameListTempUsa> GameListTempUsas { get; set; } = null!;
        public virtual DbSet<OtherSiteList> OtherSiteLists { get; set; } = null!;
        public virtual DbSet<UserFavorite> UserFavorites { get; set; } = null!;
        public virtual DbSet<UserList> UserLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings:SteamConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("u1286676_Deallocate");

            modelBuilder.Entity<GameList>(entity =>
            {
                entity.HasKey(e => e.GlId)
                    .HasName("PK__GAME_LIS__E2DBDB9144DC3972");

                entity.ToTable("GAME_LIST", "dbo");

                entity.Property(e => e.GlId).HasColumnName("GL_ID");

                entity.Property(e => e.GlAvailability).HasColumnName("GL_AVAILABILITY");

                entity.Property(e => e.GlName)
                    .HasMaxLength(500)
                    .HasColumnName("GL_NAME");

                entity.Property(e => e.GlPrice).HasColumnName("GL_PRICE");

                entity.Property(e => e.GlPriceKz).HasColumnName("GL_PRICE_KZ");

                entity.Property(e => e.GlPriceRu).HasColumnName("GL_PRICE_RU");

                entity.Property(e => e.GlPriceTr).HasColumnName("GL_PRICE_TR");

                entity.Property(e => e.GlSteamId).HasColumnName("GL_STEAM_ID");
            });

            modelBuilder.Entity<GameListTempKz>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GAME_LIST_TEMP_KZ", "dbo");

                entity.Property(e => e.GltId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GLT_ID");

                entity.Property(e => e.GltImagePath)
                    .IsUnicode(false)
                    .HasColumnName("GLT_IMAGE_PATH");

                entity.Property(e => e.GltPrice).HasColumnName("GLT_PRICE");

                entity.Property(e => e.GltSteamId).HasColumnName("GLT_STEAM_ID");
            });

            modelBuilder.Entity<GameListTempRu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GAME_LIST_TEMP_RU", "dbo");

                entity.Property(e => e.GltId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GLT_ID");

                entity.Property(e => e.GltImagePath)
                    .IsUnicode(false)
                    .HasColumnName("GLT_IMAGE_PATH");

                entity.Property(e => e.GltPrice).HasColumnName("GLT_PRICE");

                entity.Property(e => e.GltSteamId).HasColumnName("GLT_STEAM_ID");
            });

            modelBuilder.Entity<GameListTempTr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GAME_LIST_TEMP_TR", "dbo");

                entity.Property(e => e.GltId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GLT_ID");

                entity.Property(e => e.GltImagePath)
                    .IsUnicode(false)
                    .HasColumnName("GLT_IMAGE_PATH");

                entity.Property(e => e.GltPrice).HasColumnName("GLT_PRICE");

                entity.Property(e => e.GltSteamId).HasColumnName("GLT_STEAM_ID");
            });

            modelBuilder.Entity<GameListTempUsa>(entity =>
            {
                entity.HasKey(e => e.GltId)
                    .HasName("PK__GAME_LIS__EA70666574FF3FCD");

                entity.ToTable("GAME_LIST_TEMP_USA", "dbo");

                entity.Property(e => e.GltId).HasColumnName("GLT_ID");

                entity.Property(e => e.GltImagePath)
                    .IsUnicode(false)
                    .HasColumnName("GLT_IMAGE_PATH");

                entity.Property(e => e.GltPrice).HasColumnName("GLT_PRICE");

                entity.Property(e => e.GltSteamId).HasColumnName("GLT_STEAM_ID");
            });

            modelBuilder.Entity<OtherSiteList>(entity =>
            {
                entity.HasKey(e => e.OslId)
                    .HasName("PK__OTHER_SI__A46D1E121331F343");

                entity.ToTable("OTHER_SITE_LIST", "dbo");

                entity.Property(e => e.OslId).HasColumnName("OSL_ID");

                entity.Property(e => e.OslName)
                    .HasMaxLength(400)
                    .HasColumnName("OSL_NAME");

                entity.Property(e => e.OslPrice).HasColumnName("OSL_PRICE");

                entity.Property(e => e.OslRef)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("OSL_REF");

                entity.Property(e => e.OslSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OSL_SITE_NAME");

                entity.Property(e => e.OslSteamId).HasColumnName("OSL_STEAM_ID");
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => e.UfId)
                    .HasName("PK__USER_FAV__8FD45A596343B8CC");

                entity.ToTable("USER_FAVORITES", "dbo");

                entity.Property(e => e.UfId).HasColumnName("UF_ID");

                entity.Property(e => e.UfGlSteamId).HasColumnName("UF_GL_STEAM_ID");

                entity.Property(e => e.UfUlId).HasColumnName("UF_UL_ID");
            });

            modelBuilder.Entity<UserList>(entity =>
            {
                entity.HasKey(e => e.UlId)
                    .HasName("PK__USER_LIS__FB8CA7AEF298C961");

                entity.ToTable("USER_LIST", "dbo");

                entity.Property(e => e.UlId).HasColumnName("UL_ID");

                entity.Property(e => e.UlBalance).HasColumnName("UL_BALANCE");

                entity.Property(e => e.UlLastVisit)
                    .HasColumnType("datetime")
                    .HasColumnName("UL_LAST_VISIT");

                entity.Property(e => e.UlLogin)
                    .HasMaxLength(100)
                    .HasColumnName("UL_LOGIN");

                entity.Property(e => e.UlPassword)
                    .HasMaxLength(100)
                    .HasColumnName("UL_PASSWORD");
            });
            modelBuilder.Entity<JsonRes>().HasNoKey();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
