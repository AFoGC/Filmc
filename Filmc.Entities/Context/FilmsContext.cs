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

        public FilmsContext(DbContextOptions options) : base(options)
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
        public virtual DbSet<BookReadProgress> BookReadProgresses { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<FilmCategory> FilmCategories { get; set; } = null!;
        public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;
        public virtual DbSet<FilmSource> FilmSources { get; set; } = null!;
        public virtual DbSet<FilmTag> FilmTags { get; set; } = null!;
        public virtual DbSet<FilmsInPriority> FilmsInPriorities { get; set; } = null!;
        public virtual DbSet<FilmWatchProgress> FilmWatchProgresses { get; set; } = null!;

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

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Author)
                    .HasColumnName("Author")
                    .HasField("_author")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreId")
                    .HasField("_genreId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.PublicationYear)
                    .HasColumnName("PublicationYear")
                    .HasField("_publicationYear")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.ReadProgressId)
                    .HasColumnName("ReadProgressId")
                    .HasField("_readProgressId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.StartReadDate)
                    .HasColumnName("StartReadDate")
                    .HasField("_startReadDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.EndReadDate)
                    .HasColumnName("EndReadDate")
                    .HasField("_endReadDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");

                entity.Property(e => e.Comment)
                    .HasColumnName("Comment")
                    .HasField("_comment")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CountOfReadings)
                    .HasColumnName("CountOfReadings")
                    .HasField("_countOfReadings")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Bookmark)
                    .HasColumnName("Bookmark")
                    .HasField("_bookmark")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryId")
                    .HasField("_categoryId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CategoryListId)
                    .HasColumnName("CategoryListId")
                    .HasField("_categoryListId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.IsOnTheBlacklist)
                    .HasColumnName("IsOnTheBlacklist")
                    .HasField("_isOnTheBlacklist")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnSecretMode)
                    .HasColumnName("IsOnSecretMode")
                    .HasField("_isOnSecretMode")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ReadProgress)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.ReadProgressId);

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

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.HideName)
                    .HasColumnName("HideName")
                    .HasField("_hideName")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookGenres_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<BookSource>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookSources_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.BookId)
                    .HasColumnName("BookId")
                    .HasField("_bookId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.IndexInList)
                    .HasColumnName("IndexInList")
                    .HasField("_indexInList")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Url)
                    .HasColumnName("Url")
                    .HasField("_url")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookTag>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookTags_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<BooksInPriority>(entity =>
            {
                entity.ToTable("BooksInPriority");

                entity.HasIndex(e => e.Id, "IX_BooksInPriority_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CreationTime)
                    .HasColumnName("CreationDate")
                    .HasField("_creationDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v.ToString(), v => DateTime.Parse(v));

                entity.HasOne(d => d.Book)
                    .WithOne(p => p.Priority)
                    .HasForeignKey<BooksInPriority>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookReadProgress>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BookReadProgresses_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Films_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreId")
                    .HasField("_genreId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.RealiseYear)
                    .HasColumnName("RealiseYear")
                    .HasField("_realiseYear")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.WatchProgressId)
                    .HasColumnName("WatchProgressId")
                    .HasField("_watchProgressId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");

                entity.Property(e => e.EndWatchDate)
                    .HasColumnName("EndWatchDate")
                    .HasField("_endWatchDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.Comment)
                    .HasColumnName("Comment")
                    .HasField("_comment")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CountOfViews)
                    .HasColumnName("CountOfViews")
                    .HasField("_countOfViews")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryId")
                    .HasField("_categoryId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CategoryListId)
                    .HasColumnName("CategoryListId")
                    .HasField("_categoryListId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.IsOnTheBlacklist)
                    .HasColumnName("IsOnTheBlacklist")
                    .HasField("_isOnTheBlacklist")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.IsOnSecretMode)
                    .HasColumnName("IsOnSecretMode")
                    .HasField("_isOnSecretMode")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);

                entity.Property(e => e.StartWatchDate)
                    .HasColumnName("StartWatchDate")
                    .HasField("_startWatchDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v != null ? v.ToString() : null, v => v != null ? DateTime.Parse(v) : null);

                entity.Property(e => e.WatchedSeries)
                    .HasColumnName("WatchedSeries")
                    .HasField("_watchedSeries")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.TotalSeries)
                    .HasColumnName("TotalSeries")
                    .HasField("_totalSeries")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WatchProgress)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.WatchProgressId);

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

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.HideName)
                    .HasColumnName("HideName")
                    .HasField("_hideName")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.RawMark)
                    .HasColumnName("Mark");
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmGenres_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.IsSerial)
                    .HasColumnName("IsSerial")
                    .HasField("_isSerial")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v ? 1 : 0, v => v == 1);
            });

            modelBuilder.Entity<FilmSource>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmSources_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.FilmId)
                    .HasColumnName("FilmId")
                    .HasField("_filmId")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Url)
                    .HasColumnName("Url")
                    .HasField("_url")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.IndexInList)
                    .HasColumnName("IndexInList")
                    .HasField("_indexInList")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FilmTag>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmTags_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<FilmsInPriority>(entity =>
            {
                entity.ToTable("FilmsInPriority");

                entity.HasIndex(e => e.Id, "IX_FilmsInPriority_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.CreationTime)
                    .HasColumnName("CreationDate")
                    .HasField("_creationDate")
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .HasConversion(v => v.ToString(), v => DateTime.Parse(v));

                entity.HasOne(d => d.Film)
                    .WithOne(p => p.Priority)
                    .HasForeignKey<FilmsInPriority>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FilmWatchProgress>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_FilmWatchProgresses_Id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasField("_name")
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
