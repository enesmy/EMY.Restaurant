using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mail");

            migrationBuilder.EnsureSchema(
                name: "menu");

            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "reservation");

            migrationBuilder.EnsureSchema(
                name: "authorize");

            migrationBuilder.CreateTable(
                name: "tblMailList",
                schema: "mail",
                columns: table => new
                {
                    MailListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMailList", x => x.MailListID);
                });

            migrationBuilder.CreateTable(
                name: "tblMenuCategory",
                schema: "menu",
                columns: table => new
                {
                    MenuCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderPhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoPhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMenuCategory", x => x.MenuCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblOrder",
                schema: "order",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    IsAuthorized = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthorizeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAuthorizationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrder", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "tblPhoto",
                schema: "dbo",
                columns: table => new
                {
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPhoto", x => x.PhotoID);
                });

            migrationBuilder.CreateTable(
                name: "tblReservations",
                schema: "reservation",
                columns: table => new
                {
                    ReservationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmationStatus = table.Column<int>(type: "int", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblReservations", x => x.ReservationID);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                schema: "authorize",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tblUserGroups",
                schema: "authorize",
                columns: table => new
                {
                    UserGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGroupToolTip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultUserGroup = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGroups", x => x.UserGroupID);
                });

            migrationBuilder.CreateTable(
                name: "tblMenu",
                schema: "menu",
                columns: table => new
                {
                    MenuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoThumbFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMenu", x => x.MenuID);
                    table.ForeignKey(
                        name: "FK_tblMenu_tblMenuCategory_MenuCategoryID",
                        column: x => x.MenuCategoryID,
                        principalSchema: "menu",
                        principalTable: "tblMenuCategory",
                        principalColumn: "MenuCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderItem",
                schema: "order",
                columns: table => new
                {
                    OrderItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_tblOrderItem_tblOrder_BasketID",
                        column: x => x.BasketID,
                        principalSchema: "order",
                        principalTable: "tblOrder",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserGroupRoles",
                schema: "authorize",
                columns: table => new
                {
                    UserGrpoupRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizeType = table.Column<int>(type: "int", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGroupRoles", x => x.UserGrpoupRoleID);
                    table.ForeignKey(
                        name: "FK_tblUserGroupRoles_tblUserGroups_UserGroupID",
                        column: x => x.UserGroupID,
                        principalSchema: "authorize",
                        principalTable: "tblUserGroups",
                        principalColumn: "UserGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                schema: "authorize",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordStored = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HiddenQuestionAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    LockedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WrongForceCount = table.Column<int>(type: "int", nullable: false),
                    LastWrongTryingTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HiddenQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdaterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_tblUsers_tblUserGroups_UserGroupID",
                        column: x => x.UserGroupID,
                        principalSchema: "authorize",
                        principalTable: "tblUserGroups",
                        principalColumn: "UserGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMenu_MenuCategoryID",
                schema: "menu",
                table: "tblMenu",
                column: "MenuCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderItem_BasketID",
                schema: "order",
                table: "tblOrderItem",
                column: "BasketID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGroupRoles_UserGroupID",
                schema: "authorize",
                table: "tblUserGroupRoles",
                column: "UserGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_UserGroupID",
                schema: "authorize",
                table: "tblUsers",
                column: "UserGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMailList",
                schema: "mail");

            migrationBuilder.DropTable(
                name: "tblMenu",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "tblOrderItem",
                schema: "order");

            migrationBuilder.DropTable(
                name: "tblPhoto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblReservations",
                schema: "reservation");

            migrationBuilder.DropTable(
                name: "tblRoles",
                schema: "authorize");

            migrationBuilder.DropTable(
                name: "tblUserGroupRoles",
                schema: "authorize");

            migrationBuilder.DropTable(
                name: "tblUsers",
                schema: "authorize");

            migrationBuilder.DropTable(
                name: "tblMenuCategory",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "tblOrder",
                schema: "order");

            migrationBuilder.DropTable(
                name: "tblUserGroups",
                schema: "authorize");
        }
    }
}
