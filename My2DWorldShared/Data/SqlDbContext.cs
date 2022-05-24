using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My2DWorldShared.DataEntities;
using Microsoft.EntityFrameworkCore.Design;

namespace My2DWorldShared.Data
{
    public class SqlDbContext : DbContext
    {
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

                

            /*modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Inventory)
                .WithOne(x => x.User);*/


            /*modelBuilder.Entity<ShopItemEntity>()
                .HasOne(x => x.Shop)
                .WithMany(x => x.ShopItems);*/

        }
    }
}
