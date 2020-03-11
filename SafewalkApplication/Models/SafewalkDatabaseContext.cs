using Microsoft.EntityFrameworkCore;

namespace SafewalkApplication.Models
{
    public partial class SafewalkDatabaseContext : DbContext
    {
        public SafewalkDatabaseContext()
        {
        }

        public SafewalkDatabaseContext(DbContextOptions<SafewalkDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Safewalker> Safewalker { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Walk> Walk { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Safewalker>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Safewalk__3C01D56AF63E9AB8");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HomeAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Interest).HasColumnType("text");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Walk>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DestLat).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DestLng).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DestText)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartLat).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StartLng).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StartText)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WalkerCurrLat).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.WalkerCurrLng).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WalkerEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
