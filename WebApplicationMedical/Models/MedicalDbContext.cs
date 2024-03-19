using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationMedical.Models;

public partial class MedicalDbContext : DbContext
{
    public MedicalDbContext()
    {
    }

    public MedicalDbContext(DbContextOptions<MedicalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DDiagnosis> DDiagnoses { get; set; }

    public virtual DbSet<DGender> DGenders { get; set; }

    public virtual DbSet<DHospitalizationPatient> DHospitalizationPatients { get; set; }

    public virtual DbSet<DInsurancePolicy> DInsurancePolicies { get; set; }

    public virtual DbSet<DMedcard> DMedcards { get; set; }

    public virtual DbSet<DPatient> DPatients { get; set; }

    public virtual DbSet<DPatientDiagnosis> DPatientDiagnoses { get; set; }

    public virtual DbSet<DPatientDisease> DPatientDiseases { get; set; }

    public virtual DbSet<DPatientVisit> DPatientVisits { get; set; }

    public virtual DbSet<DTherapeuticPatient> DTherapeuticPatients { get; set; }

    public virtual DbSet<DTherapeuticService> DTherapeuticServices { get; set; }

    public virtual DbSet<DTherapeuticType> DTherapeuticTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=zalman; database=Medical_db; trusted_connection=true; trustservercertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DDiagnosis>(entity =>
        {
            entity.ToTable("D_Diagnosis");
        });

        modelBuilder.Entity<DGender>(entity =>
        {
            entity.ToTable("D_Gender");

            entity.Property(e => e.Gender).HasMaxLength(50);
        });

        modelBuilder.Entity<DHospitalizationPatient>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.Date });

            entity.ToTable("D_HospitalizationPatient");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Patient).WithMany(p => p.DHospitalizationPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D_HospitalizationPatient_D_Patient");
        });

        modelBuilder.Entity<DInsurancePolicy>(entity =>
        {
            entity.ToTable("D_InsurancePolicy");

            entity.Property(e => e.Id).HasMaxLength(16);
            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<DMedcard>(entity =>
        {
            entity.ToTable("D_Medcard");

            entity.Property(e => e.Id).HasMaxLength(11);
            entity.Property(e => e.Date)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<DPatient>(entity =>
        {
            entity.ToTable("D_Patient");

            entity.Property(e => e.InsurancePolicyId).HasMaxLength(16);
            entity.Property(e => e.MedcardId).HasMaxLength(11);
            entity.Property(e => e.Passport).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.DPatients)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D_Patient_D_Gender");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.DPatients)
                .HasForeignKey(d => d.InsurancePolicyId)
                .HasConstraintName("FK_D_Patient_D_InsurancePolicy1");

            entity.HasOne(d => d.Medcard).WithMany(p => p.DPatients)
                .HasForeignKey(d => d.MedcardId)
                .HasConstraintName("FK_D_Patient_D_Medcard");
        });

        modelBuilder.Entity<DPatientDiagnosis>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.DiagnosisId, e.Date });

            entity.ToTable("D_PatientDiagnosis");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Diagnosis).WithMany(p => p.DPatientDiagnoses)
                .HasForeignKey(d => d.DiagnosisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D_PatientDiagnosis_D_Diagnosis");

            entity.HasOne(d => d.Patient).WithMany(p => p.DPatientDiagnoses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D_PatientDiagnosis_D_Patient");
        });

        modelBuilder.Entity<DPatientDisease>(entity =>
        {
            entity.ToTable("D_PatientDisease");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Patient).WithMany(p => p.DPatientDiseases)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_D_PatientDisease_D_Patient");
        });

        modelBuilder.Entity<DPatientVisit>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.Date });

            entity.ToTable("D_PatientVisit");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Patient).WithMany(p => p.DPatientVisits)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D_PatientVisit_D_Patient");
        });

        modelBuilder.Entity<DTherapeuticPatient>(entity =>
        {
            entity.ToTable("D_TherapeuticPatient");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Patient).WithMany(p => p.DTherapeuticPatients)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_D_TherapeuticPatient_D_Patient");

            entity.HasOne(d => d.TherapeuticService).WithMany(p => p.DTherapeuticPatients)
                .HasForeignKey(d => d.TherapeuticServiceId)
                .HasConstraintName("FK_D_TherapeuticPatient_D_TherapeuticService");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.DTherapeuticPatients)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_D_TherapeuticPatient_D_TherapeuticType");
        });

        modelBuilder.Entity<DTherapeuticService>(entity =>
        {
            entity.ToTable("D_TherapeuticService");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<DTherapeuticType>(entity =>
        {
            entity.ToTable("D_TherapeuticType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
