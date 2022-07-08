using System;
using System.Collections.Generic;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataContext
{
    public partial class RH_Context : DbContext
    {
        public RH_Context()
        {
        }

        public RH_Context(DbContextOptions<RH_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CARGO> CARGO { get; set; } = null!;
        public virtual DbSet<CIUDAD> CIUDAD { get; set; } = null!;
        public virtual DbSet<DEPARTAMENTO> DEPARTAMENTO { get; set; } = null!;
        public virtual DbSet<EMPLEADO> EMPLEADO { get; set; } = null!;
        public virtual DbSet<HISTORICO> HISTORICO { get; set; } = null!;
        public virtual DbSet<LOCALIZACION> LOCALIZACION { get; set; } = null!;
        public virtual DbSet<LOCALIZACION_DEPARTAMENTO> LOCALIZACION_DEPARTAMENTO { get; set; } = null!;
        public virtual DbSet<PAIS> PAIS { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:rh_connection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CARGO>(entity =>
            {
                entity.HasKey(e => e.cargo_ID)
                    .HasName("PK_Cargo");

                entity.Property(e => e.cargo_ID).IsFixedLength();
            });

            modelBuilder.Entity<CIUDAD>(entity =>
            {
                entity.HasKey(e => e.ciud_ID)
                    .HasName("PK_Ciudad");

                entity.Property(e => e.ciud_ID).IsFixedLength();

                entity.Property(e => e.pais_ID).IsFixedLength();

                entity.HasOne(d => d.pais)
                    .WithMany(p => p.CIUDADs)
                    .HasForeignKey(d => d.pais_ID)
                    .HasConstraintName("FK_Ciudad_Pais");
            });

            modelBuilder.Entity<DEPARTAMENTO>(entity =>
            {
                entity.HasKey(e => e.dpto_ID)
                    .HasName("PK_Departamento");

                entity.Property(e => e.dpto_ID).IsFixedLength();
            });

            modelBuilder.Entity<EMPLEADO>(entity =>
            {
                entity.HasKey(e => e.empl_ID)
                    .HasName("PK_Empleado");

                entity.Property(e => e.empl_cargo_ID).IsFixedLength();

                entity.Property(e => e.empl_dpto_ID).IsFixedLength();

                entity.HasOne(d => d.empl_Gerente)
                    .WithMany(p => p.Inverseempl_Gerente)
                    .HasForeignKey(d => d.empl_Gerente_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_Gerente");

                entity.HasOne(d => d.empl_cargo)
                    .WithMany(p => p.EMPLEADOs)
                    .HasForeignKey(d => d.empl_cargo_ID)
                    .HasConstraintName("FK_Empleado_Cargo");

                entity.HasOne(d => d.empl_dpto)
                    .WithMany(p => p.EMPLEADOs)
                    .HasForeignKey(d => d.empl_dpto_ID)
                    .HasConstraintName("FK_Empleado_Departamento");
            });

            modelBuilder.Entity<HISTORICO>(entity =>
            {
                entity.HasKey(e => e.emphist_ID)
                    .HasName("PK_Historico");

                entity.Property(e => e.emphist_cargo_ID).IsFixedLength();

                entity.Property(e => e.emphist_dpto_ID).IsFixedLength();

                entity.HasOne(d => d.emphist_cargo)
                    .WithMany(p => p.HISTORICOs)
                    .HasForeignKey(d => d.emphist_cargo_ID)
                    .HasConstraintName("FK_Historico_Cargo");

                entity.HasOne(d => d.emphist_dpto)
                    .WithMany(p => p.HISTORICOs)
                    .HasForeignKey(d => d.emphist_dpto_ID)
                    .HasConstraintName("FK_Historico_Departamento");

                entity.HasOne(d => d.emphist_empl)
                    .WithMany(p => p.HISTORICOs)
                    .HasForeignKey(d => d.emphist_empl_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Historico_Empleado");
            });

            modelBuilder.Entity<LOCALIZACION>(entity =>
            {
                entity.HasKey(e => e.localiz_ID)
                    .HasName("PK_localizacion");

                entity.Property(e => e.localiz_ID).IsFixedLength();

                entity.Property(e => e.ciud_ID).IsFixedLength();

                entity.HasOne(d => d.ciud)
                    .WithMany(p => p.LOCALIZACIONs)
                    .HasForeignKey(d => d.ciud_ID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Localizacion_Ciudad");
            });

            modelBuilder.Entity<LOCALIZACION_DEPARTAMENTO>(entity =>
            {
                entity.Property(e => e.dpto_ID).IsFixedLength();

                entity.Property(e => e.localiz_ID).IsFixedLength();

                entity.HasOne(d => d.dpto)
                    .WithMany()
                    .HasForeignKey(d => d.dpto_ID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Departamento_Localizacion");

                entity.HasOne(d => d.localiz)
                    .WithMany()
                    .HasForeignKey(d => d.localiz_ID)
                    .HasConstraintName("FK_Localizacion_Departamento");
            });

            modelBuilder.Entity<PAIS>(entity =>
            {
                entity.HasKey(e => e.pais_ID)
                    .HasName("PK_Pais");

                entity.Property(e => e.pais_ID).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
