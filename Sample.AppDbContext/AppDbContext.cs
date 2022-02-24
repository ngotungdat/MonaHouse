using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.Auth;
using Sample.Entities.Configuration;
using Sample.Extensions;
using Sample.Interface.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.AppDbContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Catalogue

            #endregion

            #region Auth
            modelBuilder.Entity<PermitObjects>(x => x.ToTable("PermitObjects"));
            modelBuilder.Entity<Permissions>(x => x.ToTable("Permissions"));
            modelBuilder.Entity<UserGroups>(x => x.ToTable("UserGroups"));
            modelBuilder.Entity<PermitObjectPermissions>(x => x.ToTable("PermitObjectPermissions"));
            modelBuilder.Entity<UserInGroups>(x => x.ToTable("UserInGroups"));
            #endregion

            modelBuilder.Entity<Users>(x => x.ToTable("Users"));

            modelBuilder.Entity<OTPHistories>(x => x.ToTable("OTPHistories"));
            modelBuilder.Entity<SMSEmailTemplates>(x => x.ToTable("SMSEmailTemplates"));
            modelBuilder.Entity<Area>(x => x.ToTable("Area"));
            modelBuilder.Entity<District>(x => x.ToTable("District"));
            modelBuilder.Entity<UtilitiesConfig>(x => x.ToTable("UtilitiesConfig"));
            modelBuilder.Entity<Ward>(x => x.ToTable("Ward"));
            modelBuilder.Entity<Branch>(x => x.ToTable("Branch"));
            modelBuilder.Entity<BranchUtilities>(x => x.ToTable("BranchUtilities"));
            modelBuilder.Entity<Room>(x => x.ToTable("Room"));
            modelBuilder.Entity<RoomImage>(x => x.ToTable("RoomImage"));
            modelBuilder.Entity<RoomUtilities>(x => x.ToTable("RoomUtilities"));
            modelBuilder.Entity<VehicleFeeConfig>(x => x.ToTable("VehicleFeeConfig"));
            modelBuilder.Entity<ContractConfig>(x => x.ToTable("ContractConfig"));
            modelBuilder.Entity<Contract>(x => x.ToTable("Contract"));
            modelBuilder.Entity<Customer>(x => x.ToTable("Customer"));
            modelBuilder.Entity<Vehicle>(x => x.ToTable("Vehicle"));
            modelBuilder.Entity<SampleConfig>(x => x.ToTable("SampleConfig"));

            #region Configuration
            modelBuilder.Entity<EmailConfigurations>(x => x.ToTable("EmailConfigurations"));
            modelBuilder.Entity<SMSConfigurations>(x => x.ToTable("SMSConfigurations"));
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region Catalogue

        #endregion

        #region Auth
        public DbSet<PermitObjects> PermitObjects { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<PermitObjectPermissions> PermitObjectPermissions { get; set; }
        public DbSet<UserInGroups> UserInGroups { get; set; }
        #endregion

        public DbSet<Users> Users { get; set; }
        public DbSet<OTPHistories> OTPHistories { get; set; }
        public DbSet<SMSEmailTemplates> SMSEmailTemplates { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<UtilitiesConfig> UtilitiesConfig { get; set; } //Cấu hình tiện ích
        public DbSet<Ward> Ward { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BranchUtilities> BranchUtilities { get; set; } //Tiện ích của dãy trọ
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomImage> RoomImage { get; set; }
        public DbSet<RoomUtilities> RoomUtilities { get; set; } //Tiện ích của phòng
        public DbSet<VehicleFeeConfig> VehicleFeeConfig { get; set; } //Cấu hình phí giữ xe
        public DbSet<ContractConfig> ContractConfig { get; set; } //Cấu hình hợp đồng
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<SampleConfig> SampleConfig { get; set; }

        #region Configuration
        public DbSet<EmailConfigurations> EmailConfigurations { get; set; }
        public DbSet<SMSConfigurations> SMSConfigurations { get; set; }
        #endregion
    }
}
