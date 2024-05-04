using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvtoSityApp.Mode;

public partial class KursTestContext : DbContext
{
    public KursTestContext()
    {
    }

    public KursTestContext(string connectionString) : base(GetDbContextOptions(connectionString))
    {
    }

    private static DbContextOptions<KursTestContext> GetDbContextOptions(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KursTestContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return optionsBuilder.Options;
    }

    public virtual DbSet<Avto> Avtos { get; set; }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<Brigadier> Brigadiers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Drive> Drives { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<MarkaAvto> MarkaAvtos { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<NachalnikTsekha> NachalnikTsekhas { get; set; }

    public virtual DbSet<NachalnikUchastka> NachalnikUchastkas { get; set; }

    public virtual DbSet<NodeToRepair> NodeToRepairs { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<StatusAvto> StatusAvtos { get; set; }

    public virtual DbSet<TypeAvto> TypeAvtos { get; set; }

    public virtual DbSet<TypeDrive> TypeDrives { get; set; }

    public virtual DbSet<TypeEmployee> TypeEmployees { get; set; }

    public virtual DbSet<TypeFacility> TypeFacilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4NVMSI5\\SERVER;Database=kursTest;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Avto__3214EC0786E7B665");

            entity.ToTable("Avto");

            entity.Property(e => e.Color).HasMaxLength(255);
            entity.Property(e => e.DisposalDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReceptionDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Facility).WithMany(p => p.Avtos)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK__Avto__FacilityId__49C3F6B7");

            entity.HasOne(d => d.Marka).WithMany(p => p.Avtos)
                .HasForeignKey(d => d.MarkaId)
                .HasConstraintName("FK__Avto__MarkaId__4BAC3F29");

            entity.HasOne(d => d.Status).WithMany(p => p.Avtos)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Avto__StatusId__4AB81AF0");

            entity.HasOne(d => d.TypeAvto).WithMany(p => p.Avtos)
                .HasForeignKey(d => d.TypeAvtoId)
                .HasConstraintName("FK__Avto__TypeAvtoId__4CA06362");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brigade__3214EC0765BFF8F5");

            entity.ToTable("Brigade");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);

            entity.HasOne(d => d.Company).WithMany(p => p.Brigades)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Brigade__Company__3F466844");
        });

        modelBuilder.Entity<Brigadier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brigadie__3214EC079F366CC3");

            entity.ToTable("Brigadier");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.Brigade).WithMany(p => p.Brigadiers)
                .HasForeignKey(d => d.BrigadeId)
                .HasConstraintName("FK__Brigadier__Briga__5812160E");

            entity.HasOne(d => d.Master).WithMany(p => p.Brigadiers)
                .HasForeignKey(d => d.MasterId)
                .HasConstraintName("FK__Brigadier__Maste__59063A47");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3214EC079F41236F");

            entity.ToTable("Company");

            entity.Property(e => e.License).HasMaxLength(255);
            entity.Property(e => e.Mail).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Аddress).HasMaxLength(255);
        });

        modelBuilder.Entity<Drive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drive__3214EC07312C5907");

            entity.ToTable("Drive");

            entity.Property(e => e.CargoWeight).HasColumnName("cargoWeight");
            entity.Property(e => e.Distance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FinishDrive).HasColumnType("datetime");
            entity.Property(e => e.FuelConsumption).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDrive).HasColumnType("datetime");

            entity.HasOne(d => d.Avto).WithMany(p => p.Drives)
                .HasForeignKey(d => d.AvtoId)
                .HasConstraintName("FK__Drive__AvtoId__6E01572D");

            entity.HasOne(d => d.Route).WithMany(p => p.Drives)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("FK__Drive__RouteId__6FE99F9F");

            entity.HasOne(d => d.TypeDrive).WithMany(p => p.Drives)
                .HasForeignKey(d => d.TypeDriveId)
                .HasConstraintName("FK__Drive__TypeDrive__6EF57B66");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC070B35E282");

            entity.ToTable("Employee");

            entity.Property(e => e.Experience).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.Avto).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AvtoId)
                .HasConstraintName("FK__Employee__AvtoId__5DCAEF64");

            entity.HasOne(d => d.Brigade).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BrigadeId)
                .HasConstraintName("FK__Employee__Brigad__5BE2A6F2");

            entity.HasOne(d => d.TypeEmployee).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TypeEmployeeId)
                .HasConstraintName("FK__Employee__TypeEm__5CD6CB2B");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facility__3214EC07A0AFA800");

            entity.ToTable("Facility");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);

            entity.HasOne(d => d.Company).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Facility__Compan__3B75D760");

            entity.HasOne(d => d.TypeFacility).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.TypeFacilityId)
                .HasConstraintName("FK__Facility__TypeFa__3C69FB99");
        });

        modelBuilder.Entity<MarkaAvto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marka_Av__3214EC0796C8C501");

            entity.ToTable("Marka_Avto");

            entity.Property(e => e.Marka).HasMaxLength(255);
            entity.Property(e => e.Model).HasMaxLength(255);
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Master__3214EC07B798E6B2");

            entity.ToTable("Master");

            entity.Property(e => e.NachalnikUchastkaId).HasColumnName("Nachalnik_UchastkaId");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.NachalnikUchastka).WithMany(p => p.Masters)
                .HasForeignKey(d => d.NachalnikUchastkaId)
                .HasConstraintName("FK__Master__Nachalni__5535A963");
        });

        modelBuilder.Entity<NachalnikTsekha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nachalni__3214EC07BF230FA6");

            entity.ToTable("Nachalnik_Tsekha");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.NachalnikTsekhas)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Nachalnik__Compa__4F7CD00D");
        });

        modelBuilder.Entity<NachalnikUchastka>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nachalni__3214EC071B49A042");

            entity.ToTable("Nachalnik_Uchastka");

            entity.Property(e => e.NachalnikTsekhaId).HasColumnName("Nachalnik_TsekhaId");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(13);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.NachalnikTsekha).WithMany(p => p.NachalnikUchastkas)
                .HasForeignKey(d => d.NachalnikTsekhaId)
                .HasConstraintName("FK__Nachalnik__Nacha__52593CB8");
        });

        modelBuilder.Entity<NodeToRepair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Node_To___3214EC07625D6F9E");

            entity.ToTable("Node_To_Repair");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repair__3214EC0716404204");

            entity.ToTable("Repair");

            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.TotalWorkTime).HasColumnType("datetime");

            entity.HasOne(d => d.Avto).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.AvtoId)
                .HasConstraintName("FK__Repair__AvtoId__74AE54BC");

            entity.HasOne(d => d.Employee).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Repair__Employee__75A278F5");

            entity.HasOne(d => d.Node).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.NodeId)
                .HasConstraintName("FK__Repair__NodeId__76969D2E");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Route__3214EC07453B4DEA");

            entity.ToTable("Route");

            entity.Property(e => e.Distance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndPoint).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StartPoint).HasMaxLength(255);
        });

        modelBuilder.Entity<StatusAvto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status_A__3214EC07C0B1AA92");

            entity.ToTable("Status_Avto");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TypeAvto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Type_Avt__3214EC07D27D0C7B");

            entity.ToTable("Type_Avto");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TypeDrive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Type_Dri__3214EC07E61745E8");

            entity.ToTable("Type_Drive");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TypeEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Type_Emp__3214EC079E582898");

            entity.ToTable("Type_Employee");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<TypeFacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Type_Fac__3214EC07F358D4B1");

            entity.ToTable("Type_Facility");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
