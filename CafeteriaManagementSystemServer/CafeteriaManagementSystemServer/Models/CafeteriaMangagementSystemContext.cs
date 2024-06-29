using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class CafeteriaMangagementSystemContext : DbContext
    {
        public CafeteriaMangagementSystemContext()
        {
        }

        public CafeteriaMangagementSystemContext(DbContextOptions<CafeteriaMangagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Choice> Choices { get; set; } = null!;
        public virtual DbSet<CurrentDayItem> CurrentDayItems { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FoodItem> FoodItems { get; set; } = null!;
        public virtual DbSet<MealType> MealTypes { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserNotification> UserNotifications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=CafeteriaMangagementSystem;Trusted_Connection=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Choice>(entity =>
            {
                entity.ToTable("choice");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IsChoiceAdded).HasColumnName("isChoice Added");

                entity.Property(e => e.MealTypeId).HasColumnName("mealTypeId");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.MealType)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.MealTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_choice_MealType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_choice_User1");
            });

            modelBuilder.Entity<CurrentDayItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.MenuIdForBreakfast).HasColumnName("menuIdForBreakfast");

                entity.Property(e => e.MenuIdForDinner).HasColumnName("menuIdForDinner");

                entity.Property(e => e.MenuIdForLunch).HasColumnName("menuIdForLunch");

                entity.HasOne(d => d.MenuIdForBreakfastNavigation)
                    .WithMany(p => p.CurrentDayItemMenuIdForBreakfastNavigations)
                    .HasForeignKey(d => d.MenuIdForBreakfast)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentDayItems_Menu");

                entity.HasOne(d => d.MenuIdForDinnerNavigation)
                    .WithMany(p => p.CurrentDayItemMenuIdForDinnerNavigations)
                    .HasForeignKey(d => d.MenuIdForDinner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentDayItems_Menu2");

                entity.HasOne(d => d.MenuIdForLunchNavigation)
                    .WithMany(p => p.CurrentDayItemMenuIdForLunchNavigations)
                    .HasForeignKey(d => d.MenuIdForLunch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentDayItems_Menu1");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Feedbackdate)
                    .HasColumnType("datetime")
                    .HasColumnName("feedbackdate");

                entity.Property(e => e.MealId).HasColumnName("mealId");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Menu");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_User");
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Availability).HasColumnName("availability");

                entity.Property(e => e.MealTypeId).HasColumnName("mealTypeId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<MealType>(entity =>
            {
                entity.ToTable("MealType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MealType1)
                    .HasMaxLength(50)
                    .HasColumnName("mealType");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.IsPrepared).HasColumnName("isPrepared");

                entity.Property(e => e.MealTypeId).HasColumnName("mealTypeId");

                entity.HasOne(d => d.MealType)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MealTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_MealType");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.NotificationMessage)
                    .HasMaxLength(50)
                    .HasColumnName("notificationMessage");

                entity.Property(e => e.NotificationType)
                    .HasMaxLength(50)
                    .HasColumnName("notificationType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleType)
                    .HasMaxLength(50)
                    .HasColumnName("roleType");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_role");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.ToTable("UserNotification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NotificationId).HasColumnName("notificationId");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserNotifications)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserNotification_Notification");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNotifications)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserNotification_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
