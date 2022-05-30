using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My2DWorldShared.DataEntities;
using Microsoft.EntityFrameworkCore.Design;
using My2DWorldShared.Enums;

namespace My2DWorldShared.Data
{
    public class SqlDbContext : DbContext
    {
#nullable disable
        //
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserInventoryEntity> UsersInventory { get; set; }
        //
        public DbSet<ShopEntity> Shops { get; set; }
        public DbSet<ShopItemEntity> ShopItems { get; set; }
        //
        public DbSet<ServerLocationEntity> Servers { get; set; }
        //
        public DbSet<NpcEntity> Npcs { get; set; }
        public DbSet<NpcGameEntity> NpcGames { get; set; }
        public DbSet<NpcSpeechEntity> NpcSpeeches { get; set; }
        //
        public DbSet<MapEntity> Maps { get; set; }
        public DbSet<MapExitEntity> MapExits { get; set; }
        public DbSet<MapNpcEntity> MapNpcs { get; set; }
        //
        public DbSet<ItemEntity> Items { get; set; }
        //
        public DbSet<GameEntity> Games { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions)
        {
            base.OnConfiguring(dbContextOptions);

            dbContextOptions.UseLazyLoadingProxies();

            dbContextOptions.UseMySql("server=127.0.0.1;user=root;password=;database=robolandtests;", new MySqlServerVersion(new Version(8, 0, 27)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MapExitEntity>()
                .HasOne(x => x.Map)
                .WithMany(x => x.Exits)
                .HasForeignKey(x => x.MapId);

            SeedUsers(modelBuilder);
            SeedServers(modelBuilder);
            SeedGames(modelBuilder);
            SeedNpcs(modelBuilder);
            SeedNpcGames(modelBuilder);
            SeedNpcSpeeches(modelBuilder);
            SeedMaps(modelBuilder);
            SeedMapExits(modelBuilder);
            SeedMapNpcs(modelBuilder);
            SeedItems(modelBuilder);
            SeedShops(modelBuilder);
            SeedShopItems(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    Username = "Danny",
                    Password = "228228",
                    Email = "test@test.com",
                    Level = 20,
                    Cash = 5000,
                    GoldenCoins = 200,
                    Gender = GenderType.Male,
                    EyeColor = 2,
                    SkinTone = 4,
                    Birthday = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20)),
                    Admin = 1,
                    Officer = 2
                },
                new UserEntity
                {
                    Id = 2,
                    Username = "Daniel",
                    Password = "228228",
                    Email = "test@test123.com",
                    Level = 10,
                    Cash = 2340,
                    GoldenCoins = 264,
                    Gender = GenderType.Male,
                    EyeColor = 4,
                    SkinTone = 2,
                    Birthday = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20))
                }
            );
        }

        private void SeedGames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>().HasData(
                new GameEntity
                {
                    Id = 1,
                    Name = "הקפצת כדור",
                    FilePath = "Games/BounceBall"
                }
            );
        }

        private void SeedServers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServerLocationEntity>().HasData(
                new ServerLocationEntity
                {
                    Id = 1,
                    ServerName = "ראשי",
                    ServerMaxPlayers = 200
                },
                new ServerLocationEntity
                {
                    Id = 2,
                    ServerName = "בדיקות",
                    ServerMaxPlayers = 20
                }
            );
        }

        private void SeedMaps(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapEntity>().HasData(
                new MapEntity
                {
                    Id = 1,
                    Name = "חוות היער",
                    FilePath = "Maps/ForestRoom",
                    SpawnX = 11,
                    SpawnY = -1.5f,
                },
                new MapEntity
                {
                    Id = 2,
                    Name = "חנות חוות היער",
                    FilePath = "Maps/ForestRoomHub",
                    SpawnX = 10,
                    SpawnY = 3,
                },
                new MapEntity
                {
                    Id = 3,
                    Name = "היער אל העיר",
                    FilePath = "Maps/ForestToCity",
                    SpawnX = 0,
                    SpawnY = 0,
                },
                new MapEntity
                {
                    Id = 4,
                    Name = "גשר החיבור",
                    FilePath = "Maps/BridgeConnector",
                    SpawnX = -7,
                    SpawnY = -0.5f,
                }
            );
        }

        private void SeedMapNpcs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapNpcEntity>().HasData(
                new MapNpcEntity
                {
                    Id = 1,
                    MapId = 2,
                    NpcId = 1,
                    PositionX = -12,
                    PositionY = 2,
                    ScaleX = -0.95f,
                    ScaleY = 0.95f
                }
            );
        }

        private void SeedMapExits(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapExitEntity>().HasData(
                new MapExitEntity
                {
                    Id = 1,
                    MapId = 1,
                    MapExitId = 2,
                    ArrowAngle = 180,
                    EntranceX = -9,
                    EntranceY = 0,
                    ExitTeleportX = -0.15f,
                    ExitTeleportY = 3
                },
                new MapExitEntity
                {
                    Id = 2,
                    MapId = 1,
                    MapExitId = 3,
                    ArrowAngle = 90,
                    EntranceX = -13,
                    EntranceY = -4,
                    ExitTeleportX = 12,
                    ExitTeleportY = -2
                },
                new MapExitEntity
                {
                    Id = 3,
                    MapId = 2,
                    MapExitId = 1,
                    ArrowAngle = 0,
                    EntranceX = -0.15f,
                    EntranceY = 6,
                    ExitTeleportX = -9,
                    ExitTeleportY = -2
                },
                new MapExitEntity
                {
                    Id = 4,
                    MapId = 3,
                    MapExitId = 1,
                    ArrowAngle = -80,
                    EntranceX = 13,
                    EntranceY = -2,
                    ExitTeleportX = -12,
                    ExitTeleportY = -4
                },
                new MapExitEntity
                {
                    Id = 5,
                    MapId = 3,
                    MapExitId = 4,
                    ArrowAngle = 80,
                    EntranceX = -13,
                    EntranceY = 2,
                    ExitTeleportX = 11,
                    ExitTeleportY = -1
                },
                new MapExitEntity
                {
                    Id = 6,
                    MapId = 4,
                    MapExitId = 3,
                    ArrowAngle = -80,
                    EntranceX = 13,
                    EntranceY = 0,
                    ExitTeleportX = -10,
                    ExitTeleportY = 0.7f
                }
            );
        }

        private void SeedNpcs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NpcEntity>().HasData(
                new NpcEntity
                {
                    Id = 1,
                    Name = "אדגר ברי",
                    About = "היי, אני אדגר,|מוכר הבגדים של הישוב.",
                    FilePath = "Npcs/Edgar"
                }
            );
        }

        private void SeedNpcSpeeches(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NpcSpeechEntity>().HasData(
                new NpcSpeechEntity
                {
                    Id = 1,
                    NpcId = 1,
                    Speech = "חברים, יש סחורה חדשה, אני מציע לכם לבדוק."
                },
                new NpcSpeechEntity
                {
                    Id = 2,
                    NpcId = 1,
                    Speech = "יש לי כאן במחסן הרבה ביגוד שבטוח יתאים לכם."
                },
                new NpcSpeechEntity
                {
                    Id = 3,
                    NpcId = 1,
                    Speech = "המוטו שלי זה לשרת את הלקוח ברמה הכי טובה שיש."
                }
            );
        }

        private void SeedNpcGames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NpcGameEntity>().HasData(
                new NpcGameEntity
                {
                    Id = 1,
                    NpcId = 1,
                    GameId = 1
                }
            );
        }

        private void SeedItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>().HasData(
                new ItemEntity
                {
                    Id = 1,
                    Type = ItemType.Hair,
                    PriceType = PriceType.Cash,
                    Price = 600,
                    FilePath = "Hairs/HRS_01",
                    Name = "שיער קוצים",
                    ItemDesc = "בלונד"
                },
                new ItemEntity
                {
                    Id = 2,
                    Type = ItemType.Hair,
                    PriceType = PriceType.Cash,
                    Price = 500,
                    FilePath = "Hairs/HRS_02",
                    Name = "תספורת קוקו",
                    ItemDesc = "שחורה"
                },
                new ItemEntity
                {
                    Id = 3,
                    Type = ItemType.Pants,
                    PriceType = PriceType.Cash,
                    Price = 1500,
                    FilePath = "Pants/PNT_01",
                    Name = "ג'ינס",
                    ItemDesc = "כחול"
                },
                new ItemEntity
                {
                    Id = 4,
                    Type = ItemType.Hair,
                    PriceType = PriceType.Cash,
                    Price = 600,
                    FilePath = "Hairs/HRS_03",
                    Name = "תספורת מדרגה",
                    ItemDesc = "שחורה"
                },
                new ItemEntity
                {
                    Id = 5,
                    Type = ItemType.Coat,
                    PriceType = PriceType.Cash,
                    Price = 3000,
                    FilePath = "Coats/CT_01",
                    Name = "ג'אקט",
                    ItemDesc = "כחול"
                },
                new ItemEntity
                {
                    Id = 6,
                    Type = ItemType.Hovers,
                    PriceType = PriceType.Cash,
                    Price = 2500,
                    FilePath = "Hovers/HVR_01",
                    Name = "סקייטבורד",
                    ItemDesc = "כחול"
                },
                new ItemEntity
                {
                    Id = 7,
                    Type = ItemType.FacialWear,
                    PriceType = PriceType.Cash,
                    Price = 900,
                    FilePath = "Facewear/FCW_01",
                    Name = "משקפיים",
                    ItemDesc = "רואים את העולם בכחול"
                },
                new ItemEntity
                {
                    Id = 8,
                    Type = ItemType.Hat,
                    PriceType = PriceType.Cash,
                    Price = 1200,
                    FilePath = "Hats/HT_01",
                    Name = "כובע מצחיה",
                    ItemDesc = "לבן עם שחור"
                },
                new ItemEntity
                {
                    Id = 9,
                    Type = ItemType.Shoes,
                    PriceType = PriceType.Cash,
                    Price = 1200,
                    FilePath = "Shoes/SHS_01",
                    Name = "נעלי אופנה",
                    ItemDesc = "שחור לבן"
                },
                new ItemEntity
                {
                    Id = 10,
                    Type = ItemType.Top,
                    PriceType = PriceType.Cash,
                    Price = 600,
                    FilePath = "Tops/TPS_01",
                    Name = "חולצת פרפרים",
                    ItemDesc = "בצבעי הקשת"
                },
                new ItemEntity
                {
                    Id = 11,
                    Type = ItemType.Hovers,
                    PriceType = PriceType.Cash,
                    Price = 3000,
                    FilePath = "Hovers/HVR_02",
                    Name = "טיל בליסטי",
                    ItemDesc = "ירוק להסוואה מושלמת"
                },
                new ItemEntity
                {
                    Id = 12,
                    Type = ItemType.Hovers,
                    PriceType = PriceType.Cash,
                    Price = 3500,
                    FilePath = "Hovers/HVR_03",
                    Name = "הוברבורד",
                    ItemDesc = "כחול ניאון"
                },
                new ItemEntity
                {
                    Id = 13,
                    Type = ItemType.Necklace,
                    PriceType = PriceType.Cash,
                    Price = 2000,
                    FilePath = "Neckwear/NCW_01",
                    Name = "שרשרת השמש",
                    ItemDesc = "רק לכוכבים שבינינו"
                },
                new ItemEntity
                {
                    Id = 14,
                    Type = ItemType.Hat,
                    PriceType = PriceType.Cash,
                    Price = 1500,
                    FilePath = "Hats/HT_02",
                    Name = "כובע הפוך",
                    ItemDesc = "אדום"
                }
            );
        }

        private void SeedShops(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopEntity>().HasData(
                new ShopEntity
                {
                    Id = 1,
                    ShopName = "בגדי סטייל"
                }
            );
        }

        private void SeedShopItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopItemEntity>().HasData(
                new ShopItemEntity
                {
                    Id = 1,            
                    ShopId = 1,
                    ItemId = 1
                },
                new ShopItemEntity
                {
                    Id = 2,
                    ShopId = 1,
                    ItemId = 2
                },
                new ShopItemEntity
                {
                    Id = 3,
                    ShopId = 1,
                    ItemId = 3
                },
                new ShopItemEntity
                {
                    Id = 4,
                    ShopId = 1,
                    ItemId = 4
                },
                new ShopItemEntity
                {
                    Id = 5,
                    ShopId = 1,
                    ItemId = 5
                },
                new ShopItemEntity
                {
                    Id = 6,
                    ShopId = 1,
                    ItemId = 6
                },
                new ShopItemEntity
                {
                    Id = 7,
                    ShopId = 1,
                    ItemId = 7
                },
                new ShopItemEntity
                {
                    Id = 8,
                    ShopId = 1,
                    ItemId = 8
                },
                new ShopItemEntity
                {
                    Id = 9,
                    ShopId = 1,
                    ItemId = 9
                },
                new ShopItemEntity
                {
                    Id = 10,
                    ShopId = 1,
                    ItemId = 10
                },
                new ShopItemEntity
                {
                    Id = 11,
                    ShopId = 1,
                    ItemId = 11
                },
                new ShopItemEntity
                {
                    Id = 12,
                    ShopId = 1,
                    ItemId = 12
                },
                new ShopItemEntity
                {
                    Id = 13,
                    ShopId = 1,
                    ItemId = 13
                },
                new ShopItemEntity
                {
                    Id = 14,
                    ShopId = 1,
                    ItemId = 14
                }
            );
        }
    }
}
