﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTMDT_API.Data;

#nullable disable

namespace WebTMDT.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220218230304_add_create_update_date_to_book_add_role")]
    partial class add_create_update_date_to_book_add_role
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppUserBook", b =>
                {
                    b.Property<int>("WishlistId")
                        .HasColumnType("int");

                    b.Property<string>("WishlistUsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WishlistId", "WishlistUsersId");

                    b.HasIndex("WishlistUsersId");

                    b.ToTable("AppUserBook");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("AuthorsId")
                        .HasColumnType("int");

                    b.Property<int>("BooksId")
                        .HasColumnType("int");

                    b.HasKey("AuthorsId", "BooksId");

                    b.HasIndex("BooksId");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("int");

                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c907e276-4a36-4c04-a255-9e8ffa6f449a",
                            ConcurrencyStamp = "4f5a7c21-c883-4d08-8506-ee45f4899c67",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "0e94c83e-be78-46ec-ab13-c907a5c7f72c",
                            ConcurrencyStamp = "846c6707-59d7-4998-8058-d6d7002e773c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "5afb8d94-7964-4019-ba3e-2e9edf90f6a2",
                            ConcurrencyStamp = "ace329e7-e9df-43f9-a010-9ff1c38d75e7",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "e0963012-613e-43ee-9f01-8e63ef1c5568",
                            ConcurrencyStamp = "180499a0-dc28-4f94-9ed8-c9583aa1723a",
                            Name = "Shipper",
                            NormalizedName = "SHIPPER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("imgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPage")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("PromotionInfoID")
                        .HasColumnType("int");

                    b.Property<int>("PublishYear")
                        .HasColumnType("int");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("imgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromotionInfoID")
                        .IsUnique()
                        .HasFilter("[PromotionInfoID] IS NOT NULL");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.DiscountCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountPercent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DiscountCodes");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CMND")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("imgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShipperId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DiscountCodeID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipperID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalItem")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCodeID")
                        .IsUnique()
                        .HasFilter("[DiscountCodeID] IS NOT NULL");

                    b.HasIndex("ShipperID");

                    b.HasIndex("UserID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PromotionAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromotionPercent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Visible")
                        .HasColumnType("int");

                    b.Property<string>("imgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.PromotionInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PromotionAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.Property<string>("PromotionPercent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.ToTable("PromotionInfo");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Recomended")
                        .HasColumnType("bit");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("AppUserBook", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Book", null)
                        .WithMany()
                        .HasForeignKey("WishlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("WishlistUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Book", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.PromotionInfo", "PromotionInfo")
                        .WithOne("Book")
                        .HasForeignKey("WebTMDTLibrary.Data.Book", "PromotionInfoID");

                    b.HasOne("WebTMDTLibrary.Data.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PromotionInfo");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Employee", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.AppUser", "Shipper")
                        .WithMany()
                        .HasForeignKey("ShipperId");

                    b.Navigation("Shipper");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Order", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.DiscountCode", "DiscountCode")
                        .WithOne("Order")
                        .HasForeignKey("WebTMDTLibrary.Data.Order", "DiscountCodeID");

                    b.HasOne("WebTMDTLibrary.Data.AppUser", "Shipper")
                        .WithMany()
                        .HasForeignKey("ShipperID");

                    b.HasOne("WebTMDTLibrary.Data.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscountCode");

                    b.Navigation("Shipper");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.OrderDetail", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.PromotionInfo", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Promotion", "Promotion")
                        .WithMany("PromotionInfos")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Review", b =>
                {
                    b.HasOne("WebTMDTLibrary.Data.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTMDTLibrary.Data.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Book", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.DiscountCode", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Promotion", b =>
                {
                    b.Navigation("PromotionInfos");
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.PromotionInfo", b =>
                {
                    b.Navigation("Book")
                        .IsRequired();
                });

            modelBuilder.Entity("WebTMDTLibrary.Data.Publisher", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
