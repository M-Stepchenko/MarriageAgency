using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarriageAgency.Models;

public partial class MarriageAgencyContext : IdentityDbContext<User>
{
    public MarriageAgencyContext() : base()
    {
    }

    public MarriageAgencyContext(DbContextOptions<MarriageAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllService> AllServices { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FamilyStatus> FamilyStatuses { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<ProvidedService> ProvidedServices { get; set; }

    public virtual DbSet<RndView> RndViews { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<ViewClient> ViewClients { get; set; }

    public virtual DbSet<ViewService> ViewServices { get; set; }

    public virtual DbSet<ZodiacSign> ZodiacSigns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DP480U7\\SQLEXPRESS;Database=MarriageAgency;Trusted_Connection=True;Encrypt=No;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AllService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AllServi__3213E83F71E9B6AA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3213E83FFE595DE6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BadHabbits)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("badHabbits");
            entity.Property(e => e.Bithdate)
                .HasColumnType("date")
                .HasColumnName("bithdate");
            entity.Property(e => e.Children).HasColumnName("children");
            entity.Property(e => e.DesiredPartner)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("desiredPartner");
            entity.Property(e => e.FamilyStatusId).HasColumnName("familyStatusID");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Hobby)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hobby");
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("job");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NationalityId).HasColumnName("nationalityID");
            entity.Property(e => e.Passport)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("passport");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Photo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("sex");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.ZodiacSignId).HasColumnName("zodiacSignID");

            entity.HasOne(d => d.FamilyStatus).WithMany(p => p.Clients)
                .HasForeignKey(d => d.FamilyStatusId)
                .HasConstraintName("FK__Clients__FamilyStatus");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Clients)
                .HasForeignKey(d => d.NationalityId)
                .HasConstraintName("FK__Clients__nationa__57DD0BE4");

            entity.HasOne(d => d.ZodiacSign).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ZodiacSignId)
                .HasConstraintName("FK__Clients__zodiacS__56E8E7AB");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FBC511744");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bithdate)
                .HasColumnType("date")
                .HasColumnName("bithdate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position");
        });

        modelBuilder.Entity<FamilyStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FamilySt__3213E83FB72F2D5A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__National__3213E83F1093310F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Remark)
                .HasColumnType("text")
                .HasColumnName("remark");
        });

        modelBuilder.Entity<ProvidedService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provided__3213E83F45EDFAF9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientID");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.ServiceId).HasColumnName("serviceID");

            entity.HasOne(d => d.Client).WithMany(p => p.ProvidedServices)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__ProvidedS__clien__12FDD1B2");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProvidedServices)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProvidedS__emplo__1209AD79");

            entity.HasOne(d => d.Service).WithMany(p => p.ProvidedServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ProvidedS__servi__13F1F5EB");
        });

        modelBuilder.Entity<RndView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("rndView");

            entity.Property(e => e.RndResult).HasColumnName("rndResult");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("staff");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CountOfProvidedServices).HasColumnName("countOfProvidedServices");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position");
        });

        modelBuilder.Entity<ViewClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewClient");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BadHabbits)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("badHabbits");
            entity.Property(e => e.Children).HasColumnName("children");
            entity.Property(e => e.DesiredPartner)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("desiredPartner");
            entity.Property(e => e.FamilyStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("familyStatus");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Hobby)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hobby");
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("job");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.Passport)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("passport");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("sex");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.ZodiacSign)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zodiacSign");
        });

        modelBuilder.Entity<ViewService>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewServices");

            entity.Property(e => e.Client)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Employee)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("serviceName");
        });

        modelBuilder.Entity<ZodiacSign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ZodiacSi__3213E83FF7202C69");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
