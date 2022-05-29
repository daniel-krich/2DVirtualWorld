using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My2DWorldShared.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PriceType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemDesc = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpawnX = table.Column<float>(type: "float", nullable: false),
                    SpawnY = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Npcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    About = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Npcs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServerName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServerMaxPlayers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MapExits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    MapExitId = table.Column<int>(type: "int", nullable: false),
                    ArrowAngle = table.Column<float>(type: "float", nullable: false),
                    EntranceX = table.Column<float>(type: "float", nullable: false),
                    EntranceY = table.Column<float>(type: "float", nullable: false),
                    ExitTeleportX = table.Column<float>(type: "float", nullable: false),
                    ExitTeleportY = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapExits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapExits_Maps_MapExitId",
                        column: x => x.MapExitId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapExits_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Exp = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    GoldenCoins = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    SkinTone = table.Column<int>(type: "int", nullable: false),
                    EyeColor = table.Column<int>(type: "int", nullable: false),
                    Hair = table.Column<int>(type: "int", nullable: true),
                    Top = table.Column<int>(type: "int", nullable: true),
                    Pants = table.Column<int>(type: "int", nullable: true),
                    Shoes = table.Column<int>(type: "int", nullable: true),
                    Coat = table.Column<int>(type: "int", nullable: true),
                    Hat = table.Column<int>(type: "int", nullable: true),
                    FacialWear = table.Column<int>(type: "int", nullable: true),
                    Necklace = table.Column<int>(type: "int", nullable: true),
                    BodySuit = table.Column<int>(type: "int", nullable: true),
                    Earings = table.Column<int>(type: "int", nullable: true),
                    Hovers = table.Column<int>(type: "int", nullable: true),
                    LastLocationId = table.Column<int>(type: "int", nullable: true),
                    Admin = table.Column<int>(type: "int", nullable: false),
                    Officer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Maps_LastLocationId",
                        column: x => x.LastLocationId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MapNpcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    NpcId = table.Column<int>(type: "int", nullable: false),
                    PositionX = table.Column<float>(type: "float", nullable: false),
                    PositionY = table.Column<float>(type: "float", nullable: false),
                    ScaleX = table.Column<float>(type: "float", nullable: false),
                    ScaleY = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapNpcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapNpcs_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapNpcs_Npcs_NpcId",
                        column: x => x.NpcId,
                        principalTable: "Npcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NpcGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NpcId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NpcGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NpcGames_Npcs_NpcId",
                        column: x => x.NpcId,
                        principalTable: "Npcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NpcSpeeches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NpcId = table.Column<int>(type: "int", nullable: false),
                    Speech = table.Column<string>(type: "varchar(90)", maxLength: 90, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpcSpeeches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NpcSpeeches_Npcs_NpcId",
                        column: x => x.NpcId,
                        principalTable: "Npcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopItems_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInventory_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersInventory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "FilePath", "Name" },
                values: new object[] { 1, "Games/BounceBall", "הקפצת כדור" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "FilePath", "ItemDesc", "Name", "Price", "PriceType", "Type" },
                values: new object[,]
                {
                    { 1, "Hairs/HRS_01", "בלונד", "שיער קוצים", 600, 0, 1 },
                    { 2, "Hairs/HRS_02", "שחורה", "תספורת קוקו", 500, 0, 1 },
                    { 3, "Pants/PNT_01", "כחול", "ג'ינס", 1500, 0, 3 },
                    { 4, "Hairs/HRS_03", "שחורה", "תספורת מדרגה", 600, 0, 1 },
                    { 5, "Coats/CT_01", "כחול", "ג'אקט", 3000, 0, 5 },
                    { 6, "Hovers/HVR_01", "כחול", "סקייטבורד", 2500, 0, 11 },
                    { 7, "Facewear/FCW_01", "רואים את העולם בכחול", "משקפיים", 900, 0, 7 },
                    { 8, "Hats/HT_01", "לבן עם שחור", "כובע מצחיה", 1200, 0, 6 },
                    { 9, "Shoes/SHS_01", "שחור לבן", "נעלי אופנה", 1200, 0, 4 },
                    { 10, "Tops/TPS_01", "בצבעי הקשת", "חולצת פרפרים", 600, 0, 2 },
                    { 11, "Hovers/HVR_02", "ירוק להסוואה מושלמת", "טיל בליסטי", 3000, 0, 11 },
                    { 12, "Hovers/HVR_03", "כחול ניאון", "הוברבורד", 3500, 0, 11 },
                    { 13, "Neckwear/NCW_01", "רק לכוכבים שבינינו", "שרשרת השמש", 2000, 0, 8 },
                    { 14, "Hats/HT_02", "אדום", "כובע הפוך", 1500, 0, 6 }
                });

            migrationBuilder.InsertData(
                table: "Maps",
                columns: new[] { "Id", "FilePath", "Name", "SpawnX", "SpawnY" },
                values: new object[,]
                {
                    { 1, "Maps/ForestRoom", "חוות היער", 11f, -1.5f },
                    { 2, "Maps/ForestRoomHub", "חנות חוות היער", 10f, 3f },
                    { 3, "Maps/ForestToCity", "היער אל העיר", 0f, 0f },
                    { 4, "Maps/BridgeConnector", "גשר החיבור", -7f, -0.5f }
                });

            migrationBuilder.InsertData(
                table: "Npcs",
                columns: new[] { "Id", "About", "FilePath", "Name" },
                values: new object[] { 1, "היי, אני אדגר,|מוכר הבגדים של הישוב.", "Npcs/Edgar", "אדגר ברי" });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "ServerMaxPlayers", "ServerName" },
                values: new object[,]
                {
                    { 1, 200, "ראשי" },
                    { 2, 20, "בדיקות" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Birthday", "BodySuit", "Cash", "Coat", "Earings", "Email", "Exp", "EyeColor", "FacialWear", "Gender", "GoldenCoins", "Hair", "Hat", "Hovers", "LastLocationId", "Level", "Necklace", "Officer", "Pants", "Password", "Shoes", "SkinTone", "Top", "Username" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2002, 6, 2, 15, 35, 53, 280, DateTimeKind.Local).AddTicks(3712), null, 5000, null, null, "test@test.com", 0, 2, null, 0, 200, null, null, null, null, 20, null, 2, null, "228228", null, 4, null, "Danny" },
                    { 2, 0, new DateTime(2002, 6, 2, 15, 35, 53, 280, DateTimeKind.Local).AddTicks(3753), null, 2340, null, null, "test@test123.com", 0, 4, null, 0, 264, null, null, null, null, 10, null, 0, null, "228228", null, 2, null, "Daniel" }
                });

            migrationBuilder.InsertData(
                table: "MapExits",
                columns: new[] { "Id", "ArrowAngle", "EntranceX", "EntranceY", "ExitTeleportX", "ExitTeleportY", "MapExitId", "MapId" },
                values: new object[,]
                {
                    { 1, 180f, -9f, 0f, -0.15f, 3f, 2, 1 },
                    { 2, 90f, -13f, -4f, 12f, -2f, 3, 1 },
                    { 3, 0f, -0.15f, 6f, -9f, -2f, 1, 2 },
                    { 4, -80f, 13f, -2f, -12f, -4f, 1, 3 },
                    { 5, 80f, -13f, 2f, 11f, -1f, 4, 3 },
                    { 6, -80f, 13f, 0f, -10f, 0.7f, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "MapNpcs",
                columns: new[] { "Id", "MapId", "NpcId", "PositionX", "PositionY", "ScaleX", "ScaleY" },
                values: new object[] { 1, 2, 1, -12f, 2f, -0.95f, 0.95f });

            migrationBuilder.InsertData(
                table: "NpcGames",
                columns: new[] { "Id", "GameId", "NpcId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "NpcSpeeches",
                columns: new[] { "Id", "NpcId", "Speech" },
                values: new object[,]
                {
                    { 1, 1, "חברים, יש סחורה חדשה, אני מציע לכם לבדוק." },
                    { 2, 1, "יש לי כאן במחסן הרבה ביגוד שבטוח יתאים לכם." },
                    { 3, 1, "המוטו שלי זה לשרת את הלקוח ברמה הכי טובה שיש." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapExits_MapExitId",
                table: "MapExits",
                column: "MapExitId");

            migrationBuilder.CreateIndex(
                name: "IX_MapExits_MapId",
                table: "MapExits",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_MapNpcs_MapId",
                table: "MapNpcs",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_MapNpcs_NpcId",
                table: "MapNpcs",
                column: "NpcId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcGames_GameId",
                table: "NpcGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcGames_NpcId",
                table: "NpcGames",
                column: "NpcId");

            migrationBuilder.CreateIndex(
                name: "IX_NpcSpeeches_NpcId",
                table: "NpcSpeeches",
                column: "NpcId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_ItemId",
                table: "ShopItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_ShopId",
                table: "ShopItems",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastLocationId",
                table: "Users",
                column: "LastLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInventory_ItemId",
                table: "UsersInventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInventory_UserId",
                table: "UsersInventory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapExits");

            migrationBuilder.DropTable(
                name: "MapNpcs");

            migrationBuilder.DropTable(
                name: "NpcGames");

            migrationBuilder.DropTable(
                name: "NpcSpeeches");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "UsersInventory");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Npcs");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
