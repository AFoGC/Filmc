﻿// <auto-generated />
using System;
using Filmc.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Filmc.Entities.Migrations
{
    [DbContext(typeof(FilmsContext))]
    [Migration("20240116144242_Films1")]
    partial class Films1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("BookHasTag", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("BookHasTags", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Bookmark")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CategoryListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("CountOfReadings")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EndReadDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsOnSecretMode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsOnTheBlacklist")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsReaded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("PublicationYear")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.Property<string>("StartReadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GenreId");

                    b.HasIndex(new[] { "Id" }, "IX_Books_Id")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HideName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BookCategories_Id")
                        .IsUnique();

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookGenre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BookGenres_Id")
                        .IsUnique();

                    b.ToTable("BookGenres");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BooksInPriority", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BooksInPriority_Id")
                        .IsUnique();

                    b.ToTable("BooksInPriority", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex(new[] { "Id" }, "IX_BookSources_Id")
                        .IsUnique();

                    b.ToTable("BookSources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BookTags_Id")
                        .IsUnique();

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CategoryListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("CountOfViews")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EndWatchDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsOnSecretMode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsOnTheBlacklist")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsWatched")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.Property<long?>("RealiseYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartWatchDate")
                        .HasColumnType("TEXT");

                    b.Property<long?>("TotalSeries")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("WatchedSeries")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GenreId");

                    b.HasIndex(new[] { "Id" }, "IX_Films_Id")
                        .IsUnique();

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HideName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmCategories_Id")
                        .IsUnique();

                    b.ToTable("FilmCategories");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmGenre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsSerial")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmGenres_Id")
                        .IsUnique();

                    b.ToTable("FilmGenres");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmsInPriority", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmsInPriority_Id")
                        .IsUnique();

                    b.ToTable("FilmsInPriority", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex(new[] { "Id" }, "IX_FilmSources_Id")
                        .IsUnique();

                    b.ToTable("FilmSources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmTags_Id")
                        .IsUnique();

                    b.ToTable("FilmTags");
                });

            modelBuilder.Entity("FilmHasTag", b =>
                {
                    b.Property<long>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FilmId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("FilmHasTags", (string)null);
                });

            modelBuilder.Entity("BookHasTag", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .IsRequired();

                    b.HasOne("Filmc.Entities.Entities.BookTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .IsRequired();
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Book", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.BookCategory", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Filmc.Entities.Entities.BookGenre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BooksInPriority", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Book", "IdNavigation")
                        .WithOne("BooksInPriority")
                        .HasForeignKey("Filmc.Entities.Entities.BooksInPriority", "Id")
                        .IsRequired();

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookSource", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Book", "Book")
                        .WithMany("BookSources")
                        .HasForeignKey("BookId")
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.FilmCategory", "Category")
                        .WithMany("Films")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Filmc.Entities.Entities.FilmGenre", "Genre")
                        .WithMany("Films")
                        .HasForeignKey("GenreId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmsInPriority", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Film", "IdNavigation")
                        .WithOne("FilmsInPriority")
                        .HasForeignKey("Filmc.Entities.Entities.FilmsInPriority", "Id")
                        .IsRequired();

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmSource", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Film", "Film")
                        .WithMany("FilmSources")
                        .HasForeignKey("FilmId")
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("FilmHasTag", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .IsRequired();

                    b.HasOne("Filmc.Entities.Entities.FilmTag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .IsRequired();
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Book", b =>
                {
                    b.Navigation("BookSources");

                    b.Navigation("BooksInPriority");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookGenre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.Navigation("FilmSources");

                    b.Navigation("FilmsInPriority");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmCategory", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmGenre", b =>
                {
                    b.Navigation("Films");
                });
#pragma warning restore 612, 618
        }
    }
}