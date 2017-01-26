﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace nscreg.data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Address_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address_part1 = table.Column<string>(nullable: true),
                    Address_part2 = table.Column<string>(nullable: true),
                    Address_part3 = table.Column<string>(nullable: true),
                    Address_part4 = table.Column<string>(nullable: true),
                    Address_part5 = table.Column<string>(nullable: true),
                    Geographical_codes = table.Column<string>(nullable: true),
                    GPS_coordinates = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Address_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessToSystemFunctions = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    StandardDataAccess = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DataAccess = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseGroups",
                columns: table => new
                {
                    RegId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActualAddressId = table.Column<int>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    DataSource = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Employees = table.Column<int>(nullable: false),
                    EmployeesDate = table.Column<DateTime>(nullable: false),
                    EmployeesFte = table.Column<int>(nullable: false),
                    EmployeesYear = table.Column<DateTime>(nullable: false),
                    EndPeriod = table.Column<DateTime>(nullable: false),
                    EntGroupType = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    ExternalIdDate = table.Column<DateTime>(nullable: false),
                    ExternalIdType = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LiqDateEnd = table.Column<DateTime>(nullable: false),
                    LiqDateStart = table.Column<DateTime>(nullable: false),
                    LiqReason = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ParrentId = table.Column<int>(nullable: true),
                    PostalAddressId = table.Column<int>(nullable: false),
                    RegIdDate = table.Column<DateTime>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    RegistrationReason = table.Column<string>(nullable: true),
                    ReorgDate = table.Column<DateTime>(nullable: false),
                    ReorgReferences = table.Column<string>(nullable: true),
                    ReorgTypeCode = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    StartPeriod = table.Column<DateTime>(nullable: false),
                    StatId = table.Column<int>(nullable: false),
                    StatIdDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    SuspensionEnd = table.Column<string>(nullable: true),
                    SuspensionStart = table.Column<string>(nullable: true),
                    TaxRegDate = table.Column<DateTime>(nullable: false),
                    TaxRegId = table.Column<int>(nullable: false),
                    TelephoneNo = table.Column<string>(nullable: true),
                    TurnoveDate = table.Column<DateTime>(nullable: false),
                    Turnover = table.Column<decimal>(nullable: false),
                    TurnoverYear = table.Column<DateTime>(nullable: false),
                    WebAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseGroups", x => x.RegId);
                    table.ForeignKey(
                        name: "FK_EnterpriseGroups_Address_ActualAddressId",
                        column: x => x.ActualAddressId,
                        principalTable: "Address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnterpriseGroups_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnterpriseGroups_EnterpriseGroups_ParrentId",
                        column: x => x.ParrentId,
                        principalTable: "EnterpriseGroups",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatisticalUnits",
                columns: table => new
                {
                    RegId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActualAddressId = table.Column<int>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    Classified = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    DataSource = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Employees = table.Column<int>(nullable: false),
                    EmployeesDate = table.Column<DateTime>(nullable: false),
                    EmployeesYear = table.Column<DateTime>(nullable: false),
                    EndPeriod = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    ExternalIdDate = table.Column<DateTime>(nullable: false),
                    ExternalIdType = table.Column<int>(nullable: false),
                    ForeignParticipation = table.Column<string>(nullable: true),
                    FreeEconZone = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LiqDate = table.Column<string>(nullable: true),
                    LiqReason = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    NumOfPeople = table.Column<int>(nullable: false),
                    ParrentId = table.Column<int>(nullable: true),
                    PostalAddressId = table.Column<int>(nullable: false),
                    RefNo = table.Column<int>(nullable: false),
                    RegIdDate = table.Column<DateTime>(nullable: false),
                    RegMainActivity = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    RegistrationReason = table.Column<string>(nullable: true),
                    ReorgDate = table.Column<DateTime>(nullable: false),
                    ReorgReferences = table.Column<string>(nullable: true),
                    ReorgTypeCode = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    StartPeriod = table.Column<DateTime>(nullable: false),
                    StatId = table.Column<int>(nullable: false),
                    StatIdDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    SuspensionEnd = table.Column<string>(nullable: true),
                    SuspensionStart = table.Column<string>(nullable: true),
                    TaxRegDate = table.Column<DateTime>(nullable: false),
                    TaxRegId = table.Column<int>(nullable: false),
                    TelephoneNo = table.Column<string>(nullable: true),
                    TurnoveDate = table.Column<DateTime>(nullable: false),
                    Turnover = table.Column<decimal>(nullable: false),
                    TurnoverYear = table.Column<DateTime>(nullable: false),
                    WebAddress = table.Column<string>(nullable: true),
                    ActualMainActivity1 = table.Column<string>(nullable: true),
                    ActualMainActivity2 = table.Column<string>(nullable: true),
                    ActualMainActivityDate = table.Column<string>(nullable: true),
                    Commercial = table.Column<bool>(nullable: true),
                    EntGroupId = table.Column<int>(nullable: true),
                    EntGroupIdDate = table.Column<DateTime>(nullable: true),
                    EntGroupRole = table.Column<string>(nullable: true),
                    ForeignCapitalCurrency = table.Column<string>(nullable: true),
                    ForeignCapitalShare = table.Column<string>(nullable: true),
                    InstSectorCode = table.Column<string>(nullable: true),
                    MunCapitalShare = table.Column<string>(nullable: true),
                    PrivCapitalShare = table.Column<string>(nullable: true),
                    StateCapitalShare = table.Column<string>(nullable: true),
                    TotalCapital = table.Column<string>(nullable: true),
                    EntRegIdDate = table.Column<DateTime>(nullable: true),
                    EnterpriseRegId = table.Column<int>(nullable: true),
                    Founders = table.Column<string>(nullable: true),
                    LegalForm = table.Column<string>(nullable: true),
                    Market = table.Column<bool>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    EnterpriseUnitRegId = table.Column<int>(nullable: true),
                    LegalUnitId = table.Column<int>(nullable: true),
                    LegalUnitIdDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticalUnits", x => x.RegId);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_Address_ActualAddressId",
                        column: x => x.ActualAddressId,
                        principalTable: "Address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_StatisticalUnits_ParrentId",
                        column: x => x.ParrentId,
                        principalTable: "StatisticalUnits",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_EnterpriseGroups_EntGroupId",
                        column: x => x.EntGroupId,
                        principalTable: "EnterpriseGroups",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_StatisticalUnits_EnterpriseRegId",
                        column: x => x.EnterpriseRegId,
                        principalTable: "StatisticalUnits",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatisticalUnits_StatisticalUnits_EnterpriseUnitRegId",
                        column: x => x.EnterpriseUnitRegId,
                        principalTable: "StatisticalUnits",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseGroups_ActualAddressId",
                table: "EnterpriseGroups",
                column: "ActualAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseGroups_AddressId",
                table: "EnterpriseGroups",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseGroups_ParrentId",
                table: "EnterpriseGroups",
                column: "ParrentId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_ActualAddressId",
                table: "StatisticalUnits",
                column: "ActualAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_AddressId",
                table: "StatisticalUnits",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_ParrentId",
                table: "StatisticalUnits",
                column: "ParrentId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_EntGroupId",
                table: "StatisticalUnits",
                column: "EntGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_EnterpriseRegId",
                table: "StatisticalUnits",
                column: "EnterpriseRegId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalUnits_EnterpriseUnitRegId",
                table: "StatisticalUnits",
                column: "EnterpriseUnitRegId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "StatisticalUnits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EnterpriseGroups");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}