using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Filmc.Entities.Context
{
    public partial class FilmsContext : DbContext
    {
        public FilmsContext()
        {

        }

        public FilmsContext(DbContextOptions<FilmsContext> options) : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookCategory> BookCategories { get; set; } = null!;
        public virtual DbSet<BookGenre> BookGenres { get; set; } = null!;
        public virtual DbSet<BookSource> BookSources { get; set; } = null!;
        public virtual DbSet<BookTag> BookTags { get; set; } = null!;
        public virtual DbSet<BooksInPriority> BooksInPriorities { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<FilmCategory> FilmCategories { get; set; } = null!;
        public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;
        public virtual DbSet<FilmSource> FilmSources { get; set; } = null!;
        public virtual DbSet<FilmTag> FilmTags { get; set; } = null!;
        public virtual DbSet<FilmsInPriority> FilmsInPriorities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Datasource=C:\\Users\\sirko\\source\\repos\\TestSqlite\\TestFilmcDb\\bin\\Debug\\net6.0\\Films.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Books_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");

                entity.Property(e => e.IsReaded)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnTheBlacklist)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnSecretMode)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.StartReadDate)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.EndReadDate)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookHasTag",
                        l => l.HasOne<BookTag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("BookId", "TagId");

                            j.ToTable("BookHasTags");
                        });
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookCategories_Id")
                    .IsUnique();

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");

                entity.Property(e => e.Id);
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookGenres_Id")
                    .IsUnique();

                entity.Property(e => e.Id);
            });

            modelBuilder.Entity<BookSource>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookSources_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookSources)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookTag>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookTags_Id")
                    .IsUnique();

                entity.Property(e => e.Id);
            });

            modelBuilder.Entity<BooksInPriority>(entity =>
            {
                entity.ToTable("BooksInPriority");

                entity.HasIndex(e => e.Id, "IX_BooksInPriority_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.CreationDate)
                    .HasConversion(v => v.ToString(), v => DateTime.Parse(v));

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.BooksInPriority)
                    .HasForeignKey<BooksInPriority>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Films_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");

                entity.Property(e => e.IsWatched)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnTheBlacklist)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnSecretMode)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.StartWatchDate)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.EndWatchDate)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Films)
                    .UsingEntity<Dictionary<string, object>>(
                        "FilmHasTag",
                        l => l.HasOne<FilmTag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Film>().WithMany().HasForeignKey("FilmId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("FilmId", "TagId");

                            j.ToTable("FilmHasTags");
                        });
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmCategories_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmGenres_Id")
                    .IsUnique();

                entity.Property(e => e.IsSerial)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.Id);
            });

            modelBuilder.Entity<FilmSource>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmSources_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmSources)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FilmTag>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmTags_Id")
                    .IsUnique();

                entity.Property(e => e.Id);
            });

            modelBuilder.Entity<FilmsInPriority>(entity =>
            {
                entity.ToTable("FilmsInPriority");

                entity.HasIndex(e => e.Id, "IX_FilmsInPriority_Id")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.CreationDate)
                    .HasConversion(v => v.ToString(), v => DateTime.Parse(v));

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.FilmsInPriority)
                    .HasForeignKey<FilmsInPriority>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
