using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestingSystem.Models;

public partial class TestingSystemDbContext : IdentityDbContext<ApplicationUser>
{
    public TestingSystemDbContext() : base()
    {
    }
    public TestingSystemDbContext(DbContextOptions<TestingSystemDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<CompletionMark> CompletionMarks { get; set; }

    public virtual DbSet<Difficulty> Difficulties { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

   // public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

   /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answers__D4825024DAF3A50D");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answers__Questio__5EBF139D");
        });

        modelBuilder.Entity<CompletionMark>(entity =>
        {
            entity.HasKey(e => e.CompletionMarkId).HasName("PK__Completi__D103376E36146DE0");

            entity.Property(e => e.CompletionMarkId).HasColumnName("CompletionMarkID");
            entity.Property(e => e.Mark).HasColumnName("CompletionMark");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Test).WithMany(p => p.CompletionMarks)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Completio__TestI__6383C8BA");

            entity.HasOne(d => d.User).WithMany(p => p.CompletionMarks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Completio__UserI__628FA481");
        });

        modelBuilder.Entity<Difficulty>(entity =>
        {
            entity.HasKey(e => e.DifficultyId).HasName("PK__Difficul__161A32079577B302");

            entity.HasIndex(e => e.Name, "UQ__Difficul__737584F6F5794362").IsUnique();

            entity.Property(e => e.DifficultyId).HasColumnName("DifficultyID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C92BF712B");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.Text)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Questions__TestI__5BE2A6F2");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__976902287723EA0B");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Results__TestID__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Results__UserID__59063A47");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Tests__8CC331006DCB1598");

            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.DifficultyId).HasColumnName("DifficultyID");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.TopicId).HasColumnName("TopicID");

            entity.HasOne(d => d.Difficulty).WithMany(p => p.Tests)
                .HasForeignKey(d => d.DifficultyId)
                .HasConstraintName("FK__Tests__Difficult__5535A963");

            entity.HasOne(d => d.Topic).WithMany(p => p.Tests)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Tests__TopicID__5441852A");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__022E0F7DDFB58306");

            entity.HasIndex(e => e.Name, "UQ__Topics__737584F6BE11C4B6").IsUnique();

            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACBF033E00");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B2AA016D8").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053482AE52CD").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
}
