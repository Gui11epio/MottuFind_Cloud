using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<TagRfid> TagsRfid { get; set; }
        public DbSet<LeitorRfid> LeitoresRfid { get; set; }
        public DbSet<LeituraRfid> LeiturasRfid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Moto -> TagRfid (1:1)
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Tag)
                .WithOne(t => t.Moto)
                .HasForeignKey<TagRfid>(t => t.MotoPlaca)
                .OnDelete(DeleteBehavior.Cascade);

            // Moto -> Patio (N:1)
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)
                .WithMany(p => p.Motos)
                .HasForeignKey(m => m.PatioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patio -> Filial (N:1)
            modelBuilder.Entity<Patio>()
                .HasOne(p => p.Filial)
                .WithMany(f => f.Patios)
                .HasForeignKey(p => p.FilialId)
                .OnDelete(DeleteBehavior.Cascade);

            // LeitorRfid -> Patio (N:1)
            modelBuilder.Entity<LeitorRfid>()
                .HasOne(l => l.Patio)
                .WithMany(p => p.Leitores)
                .HasForeignKey(l => l.PatioId);

            // LeituraRfid -> LeitorRfid (N:1)
            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.Leitor)
                .WithMany(r => r.Leituras)
                .HasForeignKey(l => l.LeitorId);

            // LeituraRfid -> TagRfid (N:1)
            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.Tag)
                .WithMany(t => t.Leituras)
                .HasForeignKey(l => l.TagId);
        }
    }
}
