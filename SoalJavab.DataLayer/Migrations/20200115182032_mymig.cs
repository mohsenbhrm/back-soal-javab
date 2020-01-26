using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoalJavab.DataLayer.Migrations
{
    public partial class mymig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Onvan = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reshtehs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Onvan = table.Column<string>(nullable: true),
                    Regdat = table.Column<DateTime>(nullable: true),
                    IsVisited = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reshtehs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Saths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Onvan = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 450, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Family = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Regdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visitedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoggedIn = table.Column<DateTimeOffset>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 450, nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Ban = table.Column<bool>(nullable: false),
                    NewReg = table.Column<bool>(nullable: false),
                    Passs = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    AddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZirReshtehs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Onvan = table.Column<string>(nullable: true),
                    Regdat = table.Column<DateTime>(nullable: true),
                    IsVisited = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReshtehId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZirReshtehs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZirReshtehs_Reshtehs_ReshtehId",
                        column: x => x.ReshtehId,
                        principalTable: "Reshtehs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matn = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    ReportTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportUsers_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SathUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<long>(nullable: false),
                    SathId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SathUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SathUsers_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SathUsers_Saths_SathId",
                        column: x => x.SathId,
                        principalTable: "Saths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessTokenHash = table.Column<string>(nullable: true),
                    AccessTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    RefreshTokenIdHash = table.Column<string>(maxLength: 450, nullable: false),
                    RefreshTokenIdHashSource = table.Column<string>(maxLength: 450, nullable: true),
                    RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReshtehUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ZirReshtehId = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReshtehUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReshtehUsers_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReshtehUsers_ZirReshtehs_ZirReshtehId",
                        column: x => x.ZirReshtehId,
                        principalTable: "ZirReshtehs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Soals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matn = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Regdat = table.Column<DateTime>(nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    ApplicationUserId = table.Column<long>(nullable: false),
                    ZirReshtehId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soals_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Soals_ZirReshtehs_ZirReshtehId",
                        column: x => x.ZirReshtehId,
                        principalTable: "ZirReshtehs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Onvan = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ZirReshtehId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_ZirReshtehs_ZirReshtehId",
                        column: x => x.ZirReshtehId,
                        principalTable: "ZirReshtehs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Javabs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matn = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    Isvisited = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    SoalId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Javabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Javabs_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Javabs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportSoals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matn = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    SoalId = table.Column<long>(nullable: true),
                    ReportTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSoals_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportSoals_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportSoals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoalFollowers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<long>(nullable: true),
                    SoalId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoalFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoalFollowers_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoalFollowers_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoalToUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsVisited = table.Column<bool>(nullable: false),
                    Isdeleted = table.Column<bool>(nullable: false),
                    Isanswered = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    SoalId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoalToUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoalToUsers_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoalToUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagSoals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagId = table.Column<long>(nullable: false),
                    Isdeleted = table.Column<bool>(nullable: false),
                    SoalId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagSoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagSoals_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagSoals_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JavabLikes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    regdate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    JavabId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavabLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JavabLikes_Javabs_JavabId",
                        column: x => x.JavabId,
                        principalTable: "Javabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JavabLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportJavabs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matn = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsVisited = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    SoalId = table.Column<long>(nullable: true),
                    ReportTypeId = table.Column<int>(nullable: true),
                    JavabId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportJavabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportJavabs_Javabs_JavabId",
                        column: x => x.JavabId,
                        principalTable: "Javabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportJavabs_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportJavabs_Soals_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportJavabs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "Ban", "DisplayName", "Family", "Image", "IsActive", "LastLoggedIn", "Mobile", "Name", "NewReg", "Passs", "Password", "Regdate", "RowVersion", "SerialNumber", "Username", "visitedDate" },
                values: new object[] { 1L, null, false, "mohsen bahrami", "bahrami", null, true, null, null, "mohsen", true, "1234", "1234", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "mohsen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_JavabLikes_JavabId",
                table: "JavabLikes",
                column: "JavabId");

            migrationBuilder.CreateIndex(
                name: "IX_JavabLikes_UserId",
                table: "JavabLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Javabs_SoalId",
                table: "Javabs",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Javabs_UserId",
                table: "Javabs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportJavabs_JavabId",
                table: "ReportJavabs",
                column: "JavabId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportJavabs_ReportTypeId",
                table: "ReportJavabs",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportJavabs_SoalId",
                table: "ReportJavabs",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportJavabs_UserId",
                table: "ReportJavabs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSoals_ReportTypeId",
                table: "ReportSoals",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSoals_SoalId",
                table: "ReportSoals",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSoals_UserId",
                table: "ReportSoals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportUsers_ReportTypeId",
                table: "ReportUsers",
                column: "ReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportUsers_UserId",
                table: "ReportUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReshtehUsers_ApplicationUserId",
                table: "ReshtehUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReshtehUsers_ZirReshtehId",
                table: "ReshtehUsers",
                column: "ZirReshtehId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SathUsers_ApplicationUserId",
                table: "SathUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SathUsers_SathId",
                table: "SathUsers",
                column: "SathId");

            migrationBuilder.CreateIndex(
                name: "IX_SoalFollowers_ApplicationUserId",
                table: "SoalFollowers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoalFollowers_SoalId",
                table: "SoalFollowers",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Soals_ApplicationUserId",
                table: "Soals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Soals_ZirReshtehId",
                table: "Soals",
                column: "ZirReshtehId");

            migrationBuilder.CreateIndex(
                name: "IX_SoalToUsers_SoalId",
                table: "SoalToUsers",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_SoalToUsers_UserId",
                table: "SoalToUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ZirReshtehId",
                table: "Tags",
                column: "ZirReshtehId");

            migrationBuilder.CreateIndex(
                name: "IX_TagSoals_SoalId",
                table: "TagSoals",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_TagSoals_TagId",
                table: "TagSoals",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZirReshtehs_ReshtehId",
                table: "ZirReshtehs",
                column: "ReshtehId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JavabLikes");

            migrationBuilder.DropTable(
                name: "ReportJavabs");

            migrationBuilder.DropTable(
                name: "ReportSoals");

            migrationBuilder.DropTable(
                name: "ReportUsers");

            migrationBuilder.DropTable(
                name: "ReshtehUsers");

            migrationBuilder.DropTable(
                name: "SathUsers");

            migrationBuilder.DropTable(
                name: "SoalFollowers");

            migrationBuilder.DropTable(
                name: "SoalToUsers");

            migrationBuilder.DropTable(
                name: "TagSoals");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Javabs");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "Saths");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Soals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ZirReshtehs");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Reshtehs");
        }
    }
}
