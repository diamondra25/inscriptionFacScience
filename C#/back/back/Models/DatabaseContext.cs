
using Microsoft.EntityFrameworkCore;

namespace back.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Mention> Mention { get; set; }
        public DbSet<Parcours> Parcours { get; set; }
        public DbSet<Niveau> Niveau { get; set; }
        public DbSet<Niveau_Parcours> Niveau_Parcours { get; set; }
        public DbSet<Candidat> Candidat { get; set; }
        public DbSet<Piece_Candidature> Piece_Candidature { get; set; }
        public DbSet<Pre_Selection> Pre_Selection { get; set; }
        public DbSet<Pre_Inscription> Pre_Inscription { get; set; }
        public DbSet<Inscription> Inscription { get; set; }
        public DbSet<Etudiant> Etudiant { get; set; }
        public DbSet<Re_Inscription> Re_Inscription { get; set; }
        public DbSet<Piece_A_Fournir> Piece_A_Fournir { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Admin>().HasKey(a => a.id);
            modelBuilder.Entity<Mention>().HasKey(m => m.id_mention);
            modelBuilder.Entity<Parcours>().HasKey(p => p.id_parcours);
            modelBuilder.Entity<Niveau>().HasKey(n => n.id_niveau);
            modelBuilder.Entity<Niveau_Parcours>().HasKey(nm => new { nm.id_niveau, nm.id_parcours });
            modelBuilder.Entity<Candidat>().HasKey(c => c.IdCandidat);
            modelBuilder.Entity<Piece_Candidature>().HasKey(r => r.IdFichier_Candidature);
            modelBuilder.Entity<Pre_Selection>().HasKey(ps => ps.IdCandidat);
            modelBuilder.Entity<Pre_Inscription>().HasKey(pi => pi.IdPre_Inscription);
            modelBuilder.Entity<Inscription>().HasKey(i => i.matricule);
            modelBuilder.Entity<Etudiant>().HasKey(e => e.matricule);
            modelBuilder.Entity<Re_Inscription>().HasKey(ri => ri.IdRe_Inscription);
            modelBuilder.Entity<Piece_A_Fournir>().HasKey(pf => pf.IdPiece_A_Fournir);

            modelBuilder.Entity<Mention>().
                HasMany(m => m.Parcours).WithOne(p => p.Mentions).
                HasForeignKey(p => p.id_mention).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Parcours>()
                .HasMany(p => p.Candidats).WithOne(c => c.Parcours)
                .HasForeignKey(c => c.id_parcours).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Niveau>()
                .HasMany(n => n.Candidats).WithOne(c => c.Niveaux)
                .HasForeignKey(c => c.id_niveau).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Niveau_Parcours>()
                .HasOne (nm => nm.Parcours).WithMany(p => p.Niveau_Parcours)
                .HasForeignKey(nm => nm.id_parcours).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Niveau_Parcours>()
                .HasOne(nm => nm.Niveaux).WithMany(n => n.Niveau_Parcours)
                .HasForeignKey(nm => nm.id_niveau).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Piece_Candidature>()
                .HasOne(r => r.Candidats).WithMany(c => c.Piece_Candidatures)
                .HasForeignKey(r => r.IdCandidat).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Pre_Selection>()
                .HasOne(ps => ps.Candidats).WithOne(c=>c.Pre_Selections)
                .HasForeignKey<Pre_Selection>(ps => ps.IdCandidat).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Pre_Inscription>()
                .HasOne(pi => pi.Pre_Selections).WithOne(ps => ps.Pre_Inscriptions)
                .HasForeignKey<Pre_Inscription>(pi => pi.IdCandidat).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Pre_Inscription>()
                .HasOne(pi => pi.Piece_A_Fournirs).WithOne(pf => pf.Pre_Inscriptions)
                .HasForeignKey<Pre_Inscription>(pi => pi.IdPiece_A_Fournir).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inscription>()
                .HasOne(i => i.Pre_Inscription).WithOne(pi=>pi.Inscriptions)
                .HasForeignKey<Inscription>(i => i.IdPre_Inscription).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Etudiant>()
                .HasOne(e => e.Niveaux).WithMany(n => n.Etudiants)
                .HasForeignKey(e => e.id_niveau).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Etudiant>()
                .HasOne(e => e.Inscriptions).WithOne(i => i.Etudiants)
                .HasForeignKey<Etudiant>(e => e.matricule).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Etudiant>()
                .HasOne(e => e.Parcours).WithMany(p=>p.Etudiants)
                .HasForeignKey(e => e.id_parcours).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Re_Inscription>()
                .HasOne(ri=>ri.Piece_A_Fournirs).WithOne(pf=>pf.Re_Inscriptions)
                .HasForeignKey<Re_Inscription>(ri=>ri.IdPiece_A_Fournir).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Re_Inscription>()
                .HasOne(ri => ri.Etudiants).WithMany(e => e.Re_Inscriptions)
                .HasForeignKey(ri => ri.matricule).OnDelete(DeleteBehavior.Cascade); ;   
        }

    }
}
