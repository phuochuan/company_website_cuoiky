using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace company_website.Models;

public partial class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Phuoc-huan;Initial Catalog=COMPANY_DB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORY__3214EC27FFE92409");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COMPANY__3214EC2764E4C18D");

            entity.ToTable("COMPANY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Facebook)
                .HasMaxLength(1000)
                .HasColumnName("FACEBOOK");
            entity.Property(e => e.FocusedfIeld)
                .HasMaxLength(1000)
                .HasColumnName("FOCUSEDfIELD");
            entity.Property(e => e.Located)
                .HasColumnType("text")
                .HasColumnName("LOCATED");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.X).HasMaxLength(1000);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DEPARTME__3214EC2772C264B9");

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("COMPANY_ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("DESCRIPTION");

            entity.HasOne(d => d.Company).WithMany(p => p.Departments)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__DEPARTMEN__COMPA__3E52440B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMPLOYEE__3214EC2764B462B6");

            entity.ToTable("EMPLOYEE");

            entity.HasIndex(e => e.UserAccountId, "UQ__EMPLOYEE__56453098A3D2F443").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.Fullname)
                .HasMaxLength(1000)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.IdNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("ID_NO");
            entity.Property(e => e.UserAccountId).HasColumnName("USER_ACCOUNT_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__EMPLOYEE__DEPART__5812160E");

            entity.HasOne(d => d.UserAccount).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.UserAccountId)
                .HasConstraintName("FK__EMPLOYEE__USER_A__59063A47");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__POST__3214EC275D10D1B3");

            entity.ToTable("POST");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Content)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Thumbnail).HasColumnName("THUMBNAIL");
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .HasColumnName("TITLE");
            entity.Property(e => e.UserAccountId).HasColumnName("USER_ACCOUNT_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__POST__CATEGORY_I__5BE2A6F2");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK__POST__USER_ACCOU__5CD6CB2B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROLE__3214EC27BAE24ACD");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SCHEDULE__3214EC27264FB104");

            entity.ToTable("SCHEDULES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DetailsTask)
                .HasMaxLength(1000)
                .HasColumnName("DETAILS_TASK");
            entity.Property(e => e.EmployeeId).HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.IdNo)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("ID_NO");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .HasColumnName("STATUS");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SCHEDULES__EMPLO__5FB337D6");

            entity.HasOne(d => d.Task).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__SCHEDULES__TASK___60A75C0F");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TASK__3214EC2707DAE29F");

            entity.ToTable("TASK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExpectedEndDate).HasColumnName("EXPECTED_END_DATE");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("NAME");
            entity.Property(e => e.StartDate).HasColumnName("START_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("STATUS");
            entity.Property(e => e.TaskDescription)
                .HasColumnType("text")
                .HasColumnName("TASK_DESCRIPTION");
            entity.Property(e => e.TeamSize).HasColumnName("TEAM_SIZE");
            entity.Property(e => e.Thumbail).HasColumnName("THUMBAIL");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_ACC__3214EC273CF89E00");

            entity.ToTable("USER_ACCOUNT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(1000)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__USER_ACCO__PASSW__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
