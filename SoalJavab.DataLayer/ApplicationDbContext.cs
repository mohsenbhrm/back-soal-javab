using SoalJavab.DomainClasses;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SoalJavab.DataLayer
{
    public class ApplicationDbContext : DbContext,IUnitOfWork
    {

        public ApplicationDbContext(DbContextOptions option)
            : base(option)
        {
        }

       #region app domains
         //public DbSet<Category> Categories { set; get; }
        public virtual DbSet<Address> Addresses { set; get; }
        public virtual DbSet<Soal> Soals { set; get; }
        public virtual DbSet<Reshteh> Reshtehs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<SoalToUser> SoalToUsers { set; get; }
        public virtual DbSet<Javab> Javabs { set; get; }
        public virtual DbSet<SoalFollower> SoalFollowers { set; get; }

        public virtual DbSet<TagSoal> TagSoals { set; get; }
        public virtual DbSet<JavabLike> JavabLikes { set; get; }
        public virtual DbSet<ZirReshteh> ZirReshtehs { set; get; }
        public virtual DbSet<ReshtehUser> ReshtehUsers { set; get; }
        public virtual DbSet<Sath> Saths { set; get; }
        public virtual DbSet<SathUser> SathUsers { set; get; }
        public  virtual DbSet<ReportType> ReportTypes { set; get; }
        public virtual DbSet<ReportUser> ReportUsers { set; get; }
        public virtual  DbSet<ReportSoal> ReportSoals { set; get; }
        public virtual DbSet<ReportJavab> ReportJavabs { set; get; }

#endregion
#region  user  domains 
        public virtual DbSet<ApplicationUser> Users { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
#endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser {
                Id = 1,
                Username = "mohsen",
                IsActive = true,
                Password = "1234",
                Passs = "1234",
                Name = "mohsen",
                DisplayName = "mohsen bahrami",
                Family="bahrami",
                Ban = false,
                NewReg = true
            });

            // base.OnModelCreating(builder);

            // builder.Entity<ApplicationUser>().ToTable("Users");
            // builder.Entity<Role>().ToTable("Roles");
            // builder.Entity<UserRole>().ToTable("UserRoles");


             // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Custom application mappings
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.SerialNumber).HasMaxLength(450);
            });

            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<UserRole>(entity =>
            {
                //entity.HasKey(e =>e.UserRoleId);// new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });

            builder.Entity<UserToken>(entity =>
            {
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);

                entity.Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();
                entity.Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
            });
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveAllChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach(var n in entities)
            {
                Addnew (n);
            }
            return entities;
            
            //return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        public void Addnew<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return null;
            //return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        //public void ForceDatabaseInitialize()
        //{
        //    Database.EnsureCreated();
        //    //this.Database.Initialize(force: true);
        //}

        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (Entry(entity).State == EntityState.Detached)
            {
                return true;
            }
            return false;
            
        }
        
    }

 
}