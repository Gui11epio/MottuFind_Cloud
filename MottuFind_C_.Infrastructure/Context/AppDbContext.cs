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
            modelBuilder.Entity<Moto>().ToTable("TB_MOTOS");
            modelBuilder.Entity<Patio>().ToTable("TB_PATIOS");
            modelBuilder.Entity<Filial>().ToTable("TB_FILIAIS");
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIO");
            modelBuilder.Entity<TagRfid>().ToTable("TB_TAGS_RFID");
            modelBuilder.Entity<LeitorRfid>().ToTable("TB_LEITORES_RFID");
            modelBuilder.Entity<LeituraRfid>().ToTable("TB_LEITURAS_RFID");


            modelBuilder.Entity<Moto>(entity =>
            {
                entity.ToTable("TB_MOTOS");

                entity.HasKey(m => m.Placa);
                entity.Property(m => m.Placa).HasColumnName("Placa");
                entity.Property(m => m.Modelo).HasColumnName("Modelo");
                entity.Property(m => m.Marca).HasColumnName("Marca");
                entity.Property(m => m.Status).HasColumnName("Status");
                entity.Property(m => m.PatioId).HasColumnName("PatioId");

                entity.HasOne(m => m.Patio)
                    .WithMany(p => p.Motos)
                    .HasForeignKey(m => m.PatioId);
            });


            // Relacionamento Patio -> Filial (Many-to-One)
            modelBuilder.Entity<Patio>()
                .HasOne(p => p.Filial)
                .WithMany(f => f.Patios)
                .HasForeignKey(p => p.FilialId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento TagRfid -> Moto (One-to-One)
            modelBuilder.Entity<TagRfid>(entity =>
            {
                entity.ToTable("TB_TAGS_RFID");

                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).HasColumnName("Id");
                entity.Property(t => t.CodigoIdentificacao).HasColumnName("CodigoIdentificacao");
                entity.Property(t => t.MotoPlaca).HasColumnName("MotoPlaca");

                entity.HasOne(t => t.Moto)
                    .WithOne(m => m.TagRfid)
                    .HasForeignKey<TagRfid>(t => t.MotoPlaca)
                    .HasPrincipalKey<Moto>(m => m.Placa);
            });


            // Relacionamento Leitor -> Patio (Many-to-One)
            modelBuilder.Entity<LeitorRfid>()
                .HasOne(l => l.Patio)
                .WithMany(p => p.Leitores)
                .HasForeignKey(l => l.PatioId);

            // Relacionamento Leitura -> Leitor (Many-to-One)
            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.Leitor)
                .WithMany(r => r.Leituras)
                .HasForeignKey(l => l.LeitorId);

            // Relacionamento Leitura -> TagRfid (Many-to-One)
            modelBuilder.Entity<LeituraRfid>()
                .HasOne(l => l.TagRfid)
                .WithMany()
                .HasForeignKey(l => l.TagRfidId);





        }
    }
}
