namespace ORM
{
    using System.Data.Entity;
    using ORM.Entities;

    public partial class EntityModel : DbContext
    {
        /*
        public EntityModel()
            : base("name=EntityModel")
        {
        }
         * */

        public EntityModel(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<ModerationStatus> ModerationStatuses { get; set; }

        // Сделать отдельные файлы конфигураций и убрать атрибуты
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                //.HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles);
            */
            
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.Category)
                //.HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Lot)
                //.HasForeignKey(e => e.LotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.User)
                //.HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<User>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.Moderator)
                .HasForeignKey(e => e.ModeratorId)
                .WillCascadeOnDelete(false);
             

            modelBuilder.Entity<ModerationStatus>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.ModerationStatus)
                .HasForeignKey(e => e.ModerationStatusId)
                .WillCascadeOnDelete(false);
            
        }
    }
}
