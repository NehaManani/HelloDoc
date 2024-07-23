using HelloDoc_Common.Enums;
using HelloDoc_Entities.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HelloDoc_DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<UserStatus> UserStatus { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<BloodType> BloodTypes { get; set; }

        public virtual DbSet<PatientDetails> PatientDetails { get; set; }

        public virtual DbSet<ProviderDetails> ProviderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            #region Seed_Data

            modelBuilder.Entity<UserRole>().HasData(
                 new UserRole { Id = 1, Role = "Admin" },
                 new UserRole { Id = 2, Role = "Patient" },
                 new UserRole { Id = 3, Role = "Provider" }
            );

            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus { Id = 1, Status = "New" },
                new UserStatus { Id = 2, Status = "Pending" },
                new UserStatus { Id = 3, Status = "Active" },
                new UserStatus { Id = 4, Status = "Conclude" },
                new UserStatus { Id = 5, Status = "Close" },
                new UserStatus { Id = 6, Status = "Unpaid" }
            );

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Title = "Male" },
                new Gender { Id = 2, Title = "Female" }
            );

            modelBuilder.Entity<BloodType>().HasData(
               new BloodType { Id = 1, BloodGroup = "A+" },
               new BloodType { Id = 2, BloodGroup = "A-" },
               new BloodType { Id = 3, BloodGroup = "B+" },
               new BloodType { Id = 4, BloodGroup = "B-" },
               new BloodType { Id = 5, BloodGroup = "AB+" },
               new BloodType { Id = 6, BloodGroup = "AB-" },
               new BloodType { Id = 7, BloodGroup = "O+" },
               new BloodType { Id = 8, BloodGroup = "O-" }
           );

            #endregion
        }

    }
}