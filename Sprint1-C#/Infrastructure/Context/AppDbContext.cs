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
        public DbSet<TagRfid> TagRfid { get; set; }
        public DbSet<LeitorRfid> LeitorRfid { get; set; }
        public DbSet<LeituraRfid> LeituraRfid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moto>().ToTable("TB_MOTOS");
            modelBuilder.Entity<Patio>().ToTable("TB_PATIOS");
            modelBuilder.Entity<Filial>().ToTable("TB_FILIAIS");
            modelBuilder.Entity<TagRfid>(entity =>
            {
                entity.ToTable("TB_TAGS_RFID");

                entity.Property(t => t.Ativa)
                    .HasColumnType("NUMBER(1)")
                    .HasConversion<int>(); // bool <-> int (0/1)
            });

            modelBuilder.Entity<LeitorRfid>().ToTable("TB_LEITORES_RFID");
            modelBuilder.Entity<LeituraRfid>().ToTable("TB_LEITURAS_RFID");

            // Relacionamentos aqui...

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Tag)
                .WithOne(t => t.Moto)
                .HasForeignKey<TagRfid>(t => t.MotoPlaca)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)
                .WithMany(p => p.Motos)
                .HasForeignKey(m => m.PatioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patio>()
                .HasOne(p => p.Filial)
                .WithMany(f => f.Patios)
                .HasForeignKey(p => p.FilialId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeitorRfid>()
                .HasOne(l => l.Patio)
                .WithMany(p => p.Leitores)
                .HasForeignKey(l => l.PatioId);

            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.Leitor)
                .WithMany(r => r.Leituras)
                .HasForeignKey(l => l.LeitorId);

            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.Tag)
                .WithMany(t => t.Leituras)
                .HasForeignKey(l => l.TagId);

            modelBuilder.Entity<TagRfid>()
                .HasOne(t => t.Moto)
                .WithOne(m => m.Tag)
                .HasForeignKey<TagRfid>(t => t.MotoPlaca)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TagRfid>()
                .Property(t => t.MotoPlaca)
                .IsRequired()
                .ValueGeneratedNever();

        }
    }
}
