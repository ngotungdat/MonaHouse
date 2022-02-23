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

        #region Configuration
        public DbSet<EmailConfigurations> EmailConfigurations { get; set; }
        public DbSet<SMSConfigurations> SMSConfigurations { get; set; }
        #endregion
    }
}
