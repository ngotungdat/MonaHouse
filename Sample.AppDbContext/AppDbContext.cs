using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.Auth;
using Sample.Entities.Catalogue;
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
            modelBuilder.Entity<Cities>(x => x.ToTable("Cities"));
            //modelBuilder.Entity<Countries>(x => x.ToTable("Countries"));
            modelBuilder.Entity<Districts>(x => x.ToTable("Districts"));
            //modelBuilder.Entity<Nations>(x => x.ToTable("Nations"));
            modelBuilder.Entity<Wards>(x => x.ToTable("Wards"));
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
            modelBuilder.Entity<UtilitiesConfig>(x => x.ToTable("UtilitiesConfig"));
            modelBuilder.Entity<Branch>(x => x.ToTable("Branch"));
            modelBuilder.Entity<BranchImage>(x => x.ToTable("BranchImage"));
            modelBuilder.Entity<BranchUtilities>(x => x.ToTable("BranchUtilities"));
            modelBuilder.Entity<Floor>(x => x.ToTable("Floor"));
            modelBuilder.Entity<Room>(x => x.ToTable("Room"));
            modelBuilder.Entity<RoomImage>(x => x.ToTable("RoomImage"));
            modelBuilder.Entity<RoomUtilities>(x => x.ToTable("RoomUtilities"));
            modelBuilder.Entity<VehicleFeeConfig>(x => x.ToTable("VehicleFeeConfig"));
            modelBuilder.Entity<ContractConfig>(x => x.ToTable("ContractConfig"));
            modelBuilder.Entity<Contract>(x => x.ToTable("Contract"));
            modelBuilder.Entity<UserInRoom>(x => x.ToTable("UserInRoom"));
            modelBuilder.Entity<Vehicle>(x => x.ToTable("Vehicle"));
            modelBuilder.Entity<SampleConfig>(x => x.ToTable("SampleConfig"));
            modelBuilder.Entity<Package>(x => x.ToTable("Package"));
            modelBuilder.Entity<NotificationUser>(x => x.ToTable("NotificationUser"));
            modelBuilder.Entity<Notification>(x => x.ToTable("Notification"));
            //
            modelBuilder.Entity<CustomerResources>(x => x.ToTable("CustomerResources"));
            modelBuilder.Entity<FeedBackType>(x=>x.ToTable("FeedBackTypes"));
            modelBuilder.Entity<License>(x=>x.ToTable("licenses"));
            modelBuilder.Entity<LicenseSample>(x => x.ToTable("LicenseSamples"));
            modelBuilder.Entity<Feedback>(x => x.ToTable("Feedbacks"));
            modelBuilder.Entity<FeedbackComment>(x => x.ToTable("FeedbackComments")); 
            modelBuilder.Entity<PaymentMethod>(x => x.ToTable("PaymentMethods"));
            modelBuilder.Entity<RoomType>(x => x.ToTable("RoomTypes"));
            modelBuilder.Entity<ElectricWaterBill>(x => x.ToTable("ElectricWaterBills"));
            modelBuilder.Entity<UserNote>(x => x.ToTable("UserNotes"));
            modelBuilder.Entity<RoomReceipt>(x => x.ToTable("RoomReceipts"));
            modelBuilder.Entity<RoomContractRepresentative>(x => x.ToTable("RoomContractRepresentatives"));
            modelBuilder.Entity<PackageOfUser>(x => x.ToTable("PackageOfUsers"));
            modelBuilder.Entity<UserImage>(x => x.ToTable("UserImages"));
            modelBuilder.Entity<DebtCollection>(x => x.ToTable("DebtCollections"));

            #region Configuration
            modelBuilder.Entity<EmailConfigurations>(x => x.ToTable("EmailConfigurations"));
            modelBuilder.Entity<SMSConfigurations>(x => x.ToTable("SMSConfigurations"));
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region Catalogue
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Wards> Wards { get; set; }
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
        public DbSet<UtilitiesConfig> UtilitiesConfig { get; set; } //Cấu hình tiện ích
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BranchImage> BranchImage { get; set; }
        public DbSet<BranchUtilities> BranchUtilities { get; set; } //Tiện ích của dãy trọ
        public DbSet<Floor> Floor { get; set; } //tầng nhà
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomImage> RoomImage { get; set; }
        public DbSet<RoomUtilities> RoomUtilities { get; set; } //Tiện ích của phòng
        public DbSet<VehicleFeeConfig> VehicleFeeConfig { get; set; } //Cấu hình phí giữ xe
        public DbSet<ContractConfig> ContractConfig { get; set; } //Cấu hình hợp đồng
        public DbSet<Contract> Contract { get; set; }
        public DbSet<UserInRoom> UserInRoom { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<SampleConfig> SampleConfig { get; set; }
        public DbSet<Package> Package { get; set; } //gói phần mềm cho thuê
        public DbSet<NotificationUser> NotificationUser { get; set; } 
        public DbSet<Notification> Notification { get; set; } 
        //
        public DbSet<CustomerResources> CustomerResources { get; set; }
        public DbSet<FeedBackType> FeedBackTypes { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseSample> LicenseSamples { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackComment> FeedbackComments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<ElectricWaterBill> ElectricWaterBills { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<RoomReceipt> RoomReceipts { get; set; }
        public DbSet<RoomContractRepresentative> RoomContractRepresentatives { get; set; }
        public DbSet<PackageOfUser> PackageOfUsers { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<DebtCollection> DebtCollections { get; set; }

        #region Configuration
        public DbSet<EmailConfigurations> EmailConfigurations { get; set; }
        public DbSet<SMSConfigurations> SMSConfigurations { get; set; }
        #endregion
    }
}
