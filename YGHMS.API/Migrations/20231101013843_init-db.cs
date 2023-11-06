using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace YGHMS.API.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(type: "longtext", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    District = table.Column<string>(type: "longtext", nullable: true),
                    Commune = table.Column<string>(type: "longtext", nullable: true),
                    Detail = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Media = table.Column<string>(type: "longtext", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estatetypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    isRead = table.Column<sbyte>(type: "tinyint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<sbyte>(type: "tinyint", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address_Publication",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.AddressId, x.MediaId });
                    table.ForeignKey(
                        name: "Address_Media_addresses_Id_fk",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Address_Media_detaillmedia_Id_fk",
                        column: x => x.MediaId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Balance = table.Column<double>(type: "double", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CoverImage = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Users_Role",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "users_Publications_Id_fk",
                        column: x => x.AvatarId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "users_Publications_Id_fk2",
                        column: x => x.CoverImage,
                        principalTable: "Publications",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    EstateTypesId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "varchar(6000)", maxLength: 6000, nullable: true),
                    Quality = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Policies = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    DetailAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "Accommodations_addresses_Id_fk",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ForeignKey_Accommodations_EstateTypes",
                        column: x => x.EstateTypesId,
                        principalTable: "estatetypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ForeignKey_Accommodations_User",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Emai = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_accounts_userer",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ForignKey_accounts_users",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SendedId = table.Column<int>(type: "int", nullable: false),
                    ReceivedId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_User",
                        column: x => x.SendedId,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_User_Receiver",
                        column: x => x.ReceivedId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_highlight",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "id")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    publication_id = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    isDelete = table.Column<sbyte>(type: "tinyint", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    modifyAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "user_highlight_Publications_Id_fk",
                        column: x => x.publication_id,
                        principalTable: "Publications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "user_highlight_users_UserId_fk",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accommodation_publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MediaId = table.Column<int>(type: "int", nullable: true),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_Media_Accommodation",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accomodation_Media_Media",
                        column: x => x.MediaId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    MaxOccupant = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: true),
                    Area = table.Column<float>(type: "float", nullable: true),
                    TypeOfBed = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    NumberOfBed = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Accom",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Apartment_User",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "follow_user_accom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccomodationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follow_Accom",
                        column: x => x.AccomodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Follow_User",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccomodationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Accommodation",
                        column: x => x.AccomodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Report_user",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    IsDelete = table.Column<sbyte>(type: "tinyint", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Transactions_Accommodations_Id_fk",
                        column: x => x.PostId,
                        principalTable: "Accommodations",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Apartment_BedType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDelete = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "Apartment_BedType_apartments_Id_fk",
                        column: x => x.ApartmentId,
                        principalTable: "apartments",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "apartment_publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Media_Apartment",
                        column: x => x.ApartmentId,
                        principalTable: "apartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Apartment_Media_Media",
                        column: x => x.MediaId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApartmentBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    isDelete = table.Column<sbyte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "ApartmentBenefits_apartments_Id_fk",
                        column: x => x.ApartmentId,
                        principalTable: "apartments",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "apartments_amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenity",
                        column: x => x.AmenityId,
                        principalTable: "amenities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Apartment",
                        column: x => x.ApartmentId,
                        principalTable: "apartments",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    BedType = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Apartment",
                        column: x => x.ApartmentId,
                        principalTable: "apartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_User",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_User_Owner",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification_follow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    FollowId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Follow_Follow",
                        column: x => x.FollowId,
                        principalTable: "follow_user_accom",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_Follow_Notification",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reason_report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReasonId = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reason_Report_Reason",
                        column: x => x.ReasonId,
                        principalTable: "reasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reason_Report_Report",
                        column: x => x.ReportId,
                        principalTable: "report",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "unavailableapartmentdate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Apartment_TypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aprartment",
                        column: x => x.Apartment_TypeId,
                        principalTable: "Apartment_BedType",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification_reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_reservation_Notification",
                        column: x => x.NotificationId,
                        principalTable: "Notification_reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_reservation_Reservation",
                        column: x => x.ReservationId,
                        principalTable: "reservation",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    Comment = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "reviews_reservation_Id_fk",
                        column: x => x.ReservationId,
                        principalTable: "reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "reviews_users_Id_fk",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification_review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NotifycatonId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Review_Notification",
                        column: x => x.NotifycatonId,
                        principalTable: "Notification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_Review_Review",
                        column: x => x.ReviewId,
                        principalTable: "reviews",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_accommodation_media_Accommdation_idx",
                table: "accommodation_publication",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "FK_Accomodation_Media_Media_idx",
                table: "accommodation_publication",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "accommodation_publication",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Accommodations_addresses_Id_fk",
                table: "Accommodations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "ForeignKey_Accommodations_EstateTypes_idx",
                table: "Accommodations",
                column: "EstateTypesId");

            migrationBuilder.CreateIndex(
                name: "ForeignKey_Accommodations_User_idx",
                table: "Accommodations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "accounts_userer_roleId_idx",
                table: "accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserId_UNIQUE",
                table: "accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserName_UNIQUE",
                table: "accounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Address_Media_detaillmedia_Id_fk",
                table: "Address_Publication",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE1",
                table: "addresses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE2",
                table: "amenities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Apartment_BedType_ApartmentId_index",
                table: "Apartment_BedType",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "Apartment_BedType_pk2",
                table: "Apartment_BedType",
                columns: new[] { "ApartmentId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Apartment_Media_Apartment_idx",
                table: "apartment_publication",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "FK_Apartment_Media_Media_idx",
                table: "apartment_publication",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE4",
                table: "apartment_publication",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ApartmentBenefits_ApartmentId_index",
                table: "ApartmentBenefits",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "FK_Apartment_Accom_idx",
                table: "apartments",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "FK_Apartment_User_idx",
                table: "apartments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE3",
                table: "apartments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Amenity_idx",
                table: "apartments_amenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "FK_Apartment_idx",
                table: "apartments_amenities",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE5",
                table: "apartments_amenities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Chat_Sender_idx",
                table: "chat",
                columns: new[] { "SendedId", "ReceivedId" });

            migrationBuilder.CreateIndex(
                name: "FK_Chat_User_Receiver_idx",
                table: "chat",
                column: "ReceivedId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "chat",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EstateType_UNIQUE",
                table: "estatetypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE7",
                table: "estatetypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Follow_Accom_idx",
                table: "follow_user_accom",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "FK_Follow_User_idx",
                table: "follow_user_accom",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE8",
                table: "follow_user_accom",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE9",
                table: "Notification",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Notification_Follow_Notification_idx",
                table: "Notification_follow",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE10",
                table: "Notification_follow",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_follow_FollowId",
                table: "Notification_follow",
                column: "FollowId");

            migrationBuilder.CreateIndex(
                name: "FK_Notification_reservation_Notification_idx",
                table: "Notification_reservation",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "FK_Notification_reservation_Reservation_idx",
                table: "Notification_reservation",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE11",
                table: "Notification_reservation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Notification_Review_Notification_idx",
                table: "Notification_review",
                column: "NotifycatonId");

            migrationBuilder.CreateIndex(
                name: "FK_Notification_Review_Review_idx",
                table: "Notification_review",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE12",
                table: "Notification_review",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE6",
                table: "Publications",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Reason_Report_Reason_idx",
                table: "reason_report",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "FK_Reason_Report_Report_idx",
                table: "reason_report",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "FK_Report_Accommodation_idx",
                table: "report",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "FK_Report_user_idx",
                table: "report",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE13",
                table: "report",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Reservation_Apartment_idx",
                table: "reservation",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "FK_Reservation_User_Owner_idx",
                table: "reservation",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "FK_User_idx",
                table: "reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idReservation_UNIQUE",
                table: "reservation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE14",
                table: "reviews",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "reviews_reservation_Id_fk",
                table: "reviews",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "reviews_users_Id_fk",
                table: "reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "Transactions_Id_index",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "Transactions_PostId_index",
                table: "Transactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "Transactions_UserId_index",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "FK_Âprartment_idx",
                table: "unavailableapartmentdate",
                column: "Apartment_TypeId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE15",
                table: "unavailableapartmentdate",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_highlight_pk2",
                table: "user_highlight",
                columns: new[] { "user_id", "publication_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_highlight_Publications_Id_fk",
                table: "user_highlight",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ForeignKey_Users_Role_idx",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "users_pk",
                table: "users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_Publications_Id_fk",
                table: "users",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "users_Publications_Id_fk2",
                table: "users",
                column: "CoverImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accommodation_publication");

            migrationBuilder.DropTable(
                name: "Address_Publication");

            migrationBuilder.DropTable(
                name: "apartment_publication");

            migrationBuilder.DropTable(
                name: "ApartmentBenefits");

            migrationBuilder.DropTable(
                name: "apartments_amenities");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "Notification_follow");

            migrationBuilder.DropTable(
                name: "Notification_reservation");

            migrationBuilder.DropTable(
                name: "Notification_review");

            migrationBuilder.DropTable(
                name: "reason_report");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "unavailableapartmentdate");

            migrationBuilder.DropTable(
                name: "user_highlight");

            migrationBuilder.DropTable(
                name: "amenities");

            migrationBuilder.DropTable(
                name: "follow_user_accom");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "reasons");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "Apartment_BedType");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "apartments");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "estatetypes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
