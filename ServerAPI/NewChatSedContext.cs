using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI;

public partial class NewChatSedContext : DbContext
{
    public NewChatSedContext()
    {
    }

    public NewChatSedContext(DbContextOptions<NewChatSedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<ContatsUser> ContatsUsers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-U9HFGMNP;Database=NewChatSed;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.ToTable("ContactMessage");

            entity.HasOne(d => d.IdContatsUserNavigation).WithMany(p => p.ContactMessages)
                .HasForeignKey(d => d.IdContatsUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ContactMessage_ContatsUser");

            entity.HasOne(d => d.IdMessageNavigation).WithMany(p => p.ContactMessages)
                .HasForeignKey(d => d.IdMessage)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ContactMessage_Message");
        });

        modelBuilder.Entity<ContatsUser>(entity =>
        {
            entity.ToTable("ContatsUser");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ContatsUsers)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ContatsUser_UserData");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Text).HasColumnType("text");
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(24);
            entity.Property(e => e.UserName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
