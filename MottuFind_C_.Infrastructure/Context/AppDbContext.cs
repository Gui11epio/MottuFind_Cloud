using Microsoft.EntityFrameworkCore;
using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Moto> Moto { get; set; }
        public DbSet<Patio> Patio { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TagRfid> TagRfid { get; set; }
        public DbSet<LeitorRfid> LeitorRfid { get; set; }
        public DbSet<LeituraRfid> LeituraRfid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==============================
            // 🏢 FILIAL
            // ==============================
            modelBuilder.Entity<Filial>(entity =>
            {
                entity.ToTable("TB_FILIAIS");
                entity.HasKey(f => f.Id);

                entity.Property(f => f.Cidade)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(f => f.Pais)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // ==============================
            // 🏠 PÁTIO
            // ==============================
            modelBuilder.Entity<Patio>(entity =>
            {
                entity.ToTable("TB_PATIOS");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.FilialId).IsRequired();

                entity.HasOne(p => p.Filial)
                    .WithMany(f => f.Patios)
                    .HasForeignKey(p => p.FilialId)
                    .OnDelete(DeleteBehavior.Cascade) // 🔥 Apaga pátios ao excluir filial
                    .HasConstraintName("FK_PATIOS_FILIAIS");
            });

            // ==============================
            // 🏍️ MOTO
            // ==============================
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.ToTable("TB_MOTOS");
                entity.HasKey(m => m.Placa);

                entity.Property(m => m.Placa)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(m => m.Marca)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(m => m.Modelo)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(m => m.Status)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(m => m.PatioId).IsRequired();

                entity.HasOne(m => m.Patio)
                    .WithMany(p => p.Motos)
                    .HasForeignKey(m => m.PatioId)
                    .OnDelete(DeleteBehavior.Cascade) // 🔥 Exclui motos ao apagar o pátio
                    .HasConstraintName("FK_MOTOS_PATIOS");
            });

            // ==============================
            // 🏷️ TAG RFID (1:1 com Moto)
            // ==============================
            modelBuilder.Entity<TagRfid>(entity =>
            {
                entity.ToTable("TB_TAGS_RFID");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.CodigoIdentificacao)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(t => t.MotoPlaca)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasIndex(t => t.MotoPlaca)
                      .IsUnique();

                entity.HasOne(t => t.Moto)
                    .WithOne(m => m.TagRfid)
                    .HasForeignKey<TagRfid>(t => t.MotoPlaca)
                    .HasPrincipalKey<Moto>(m => m.Placa)
                    .OnDelete(DeleteBehavior.Cascade) // 🔥 Remove tag quando moto for removida
                    .HasConstraintName("FK_TAGS_MOTOS");
            });

            // ==============================
            // 📡 LEITOR RFID (1:N com Pátio)
            // ==============================
            modelBuilder.Entity<LeitorRfid>(entity =>
            {
                entity.ToTable("TB_LEITORES_RFID");
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Localizacao)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(l => l.IpDispositivo)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(l => l.Patio)
                    .WithMany(p => p.Leitores)
                    .HasForeignKey(l => l.PatioId)
                    .OnDelete(DeleteBehavior.Cascade) // 🔥 Remove leitores ao apagar pátio
                    .HasConstraintName("FK_LEITORES_PATIOS");
            });

            // ==============================
            // 📈 LEITURA RFID (Histórico)
            // ==============================
            modelBuilder.Entity<LeituraRfid>(entity =>
            {
                entity.ToTable("TB_LEITURAS_RFID");
                entity.HasKey(l => l.Id);

                entity.Property(l => l.DataHora)
                    .HasColumnType("datetime2")
                    .IsRequired();

                // 🔒 MANTER HISTÓRICO: se um Leitor for removido, a leitura permanece
                entity.HasOne(l => l.Leitor)
                    .WithMany(r => r.Leituras)
                    .HasForeignKey(l => l.LeitorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LEITURAS_LEITORES");

                // 🔒 MANTER HISTÓRICO: se uma Tag for removida, leitura fica guardada
                entity.HasOne(l => l.TagRfid)
                    .WithMany()
                    .HasForeignKey(l => l.TagRfidId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LEITURAS_TAGS");
            });

            // ==============================
            // 👤 USUÁRIO
            // ==============================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("TB_USUARIOS");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.NomeUsuario)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(u => u.Senha)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(u => u.Setor)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }


    }
}
