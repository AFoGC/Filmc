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
    [Migration("20240207071130_Films3")]
    partial class Films3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("BookHasTag", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("BookHasTags", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Author");

                    b.Property<int?>("BookCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bookmark")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Bookmark");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryId");

                    b.Property<int?>("CategoryListId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryListId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Comment");

                    b.Property<int?>("CountOfReadings")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CountOfReadings");

                    b.Property<string>("EndReadDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("EndReadDate");

                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("GenreId");

                    b.Property<int>("IsOnSecretMode")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsOnSecretMode");

                    b.Property<int>("IsOnTheBlacklist")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsOnTheBlacklist");

                    b.Property<int>("IsReaded")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsReaded");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<int?>("PublicationYear")
                        .HasColumnType("INTEGER")
                        .HasColumnName("PublicationYear");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.Property<string>("StartReadDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("StartReadDate");

                    b.HasKey("Id");

                    b.HasIndex("BookCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GenreId");

                    b.HasIndex(new[] { "Id" }, "IX_Books_Id")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("HideName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("HideName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BookGenres_Id")
                        .IsUnique();

                    b.ToTable("BookGenres");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BooksInPriority", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("CreationTime")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationDate");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BooksInPriority_Id")
                        .IsUnique();

                    b.ToTable("BooksInPriority", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("BookId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex(new[] { "Id" }, "IX_BookSources_Id")
                        .IsUnique();

                    b.ToTable("BookSources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_BookTags_Id")
                        .IsUnique();

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryId");

                    b.Property<int?>("CategoryListId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryListId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Comment");

                    b.Property<int?>("CountOfViews")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CountOfViews");

                    b.Property<string>("EndWatchDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("EndWatchDate");

                    b.Property<int?>("FilmCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("GenreId");

                    b.Property<int>("IsOnSecretMode")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsOnSecretMode");

                    b.Property<int>("IsOnTheBlacklist")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsOnTheBlacklist");

                    b.Property<int>("IsWatched")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsWatched");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<int?>("RawMark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Mark");

                    b.Property<int?>("RealiseYear")
                        .HasColumnType("INTEGER")
                        .HasColumnName("RealiseYear");

                    b.Property<string>("StartWatchDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("StartWatchDate");

                    b.Property<int?>("TotalSeries")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TotalSeries");

                    b.Property<int?>("WatchedSeries")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WatchedSeries");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FilmCategoryId");

                    b.HasIndex("GenreId");

                    b.HasIndex(new[] { "Id" }, "IX_Films_Id")
                        .IsUnique();

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("HideName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("HideName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("IsSerial")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsSerial");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmGenres_Id")
                        .IsUnique();

                    b.ToTable("FilmGenres");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmsInPriority", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("CreationTime")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationDate");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmsInPriority_Id")
                        .IsUnique();

                    b.ToTable("FilmsInPriority", (string)null);
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("FilmId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("FilmId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex(new[] { "Id" }, "IX_FilmSources_Id")
                        .IsUnique();

                    b.ToTable("FilmSources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_FilmTags_Id")
                        .IsUnique();

                    b.ToTable("FilmTags");
                });

            modelBuilder.Entity("FilmHasTag", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
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
                    b.HasOne("Filmc.Entities.Entities.BookCategory", null)
                        .WithMany("CategoryBooks")
                        .HasForeignKey("BookCategoryId");

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
                    b.HasOne("Filmc.Entities.Entities.Book", "Book")
                        .WithOne("Priority")
                        .HasForeignKey("Filmc.Entities.Entities.BooksInPriority", "Id")
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookSource", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Book", "Book")
                        .WithMany("Sources")
                        .HasForeignKey("BookId")
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.FilmCategory", "Category")
                        .WithMany("Films")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Filmc.Entities.Entities.FilmCategory", null)
                        .WithMany("CategoryFilms")
                        .HasForeignKey("FilmCategoryId");

                    b.HasOne("Filmc.Entities.Entities.FilmGenre", "Genre")
                        .WithMany("Films")
                        .HasForeignKey("GenreId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmsInPriority", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Film", "Film")
                        .WithOne("Priority")
                        .HasForeignKey("Filmc.Entities.Entities.FilmsInPriority", "Id")
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmSource", b =>
                {
                    b.HasOne("Filmc.Entities.Entities.Film", "Film")
                        .WithMany("Sources")
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
                    b.Navigation("Priority");

                    b.Navigation("Sources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookCategory", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("CategoryBooks");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.BookGenre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.Film", b =>
                {
                    b.Navigation("Priority");

                    b.Navigation("Sources");
                });

            modelBuilder.Entity("Filmc.Entities.Entities.FilmCategory", b =>
                {
                    b.Navigation("CategoryFilms");

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
