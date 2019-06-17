using System.Data.Entity;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.EFCore
{
    public class WorkoutDbContext : DbContext
    {
        public WorkoutDbContext() : base("WorkoutDbContext")
        {
            //Database.SetInitializer(new WorkoutDbInitializer());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Active> Actives { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            workout_category
            */
            modelBuilder.Entity<Category>().ToTable("workout_category");
            modelBuilder.Entity<Category>().HasKey<int>(s => s.CategoryId);
            modelBuilder.Entity<Category>()
                .Property(s => s.CategoryId)
                .HasColumnName("category_id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Category>().Property(s => s.CategoryName).HasColumnName("category_name").HasMaxLength(64).IsRequired();

            /*
            workout_collection
            */
            modelBuilder.Entity<Collection>().ToTable("workout_collection");
            modelBuilder.Entity<Collection>().HasKey<int>(s => s.WorkoutId);
            modelBuilder.Entity<Collection>()
                .HasRequired<Category>(s => s.CollectionCategory)
                .WithMany(g => g.CollectionsHavingThisCategory)
                .HasForeignKey<int>(f => f.CollectionCategoryId);

            modelBuilder.Entity<Collection>().Property(s => s.WorkoutId).HasColumnName("workout_id");
            modelBuilder.Entity<Collection>().Property(s => s.WorkoutTitle).HasColumnName("workout_title").HasMaxLength(128).IsRequired();
            modelBuilder.Entity<Collection>().Property(s => s.WorkoutNote).HasColumnName("workout_note").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Collection>().Property(s => s.CaloriesBurntPerMin).HasColumnName("calories_burn_per_min").IsRequired();
            modelBuilder.Entity<Collection>().Property(s => s.CollectionCategoryId).HasColumnName("category_id");

            /*
            workout_active
            */
            modelBuilder.Entity<Active>().HasKey<int>(s => s.ActiveWorkoutId);
            modelBuilder.Entity<Collection>()
                .HasOptional(s => s.CollectionActiveData)
                .WithRequired(r => r.ActiveCollectionRecord);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
