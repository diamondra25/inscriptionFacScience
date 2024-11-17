using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace back.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "longtext", nullable: false),
                    prenom = table.Column<string>(type: "longtext", nullable: true),
                    role = table.Column<string>(type: "longtext", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mention",
                columns: table => new
                {
                    id_mention = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nom_mention = table.Column<string>(type: "longtext", nullable: false),
                    code_mention = table.Column<string>(type: "longtext", nullable: false),
                    DernierMatricule = table.Column<int>(type: "int", nullable: false),
                    DernierIdCandidat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mention", x => x.id_mention);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Niveau",
                columns: table => new
                {
                    id_niveau = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.id_niveau);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Piece_A_Fournir",
                columns: table => new
                {
                    IdPiece_A_Fournir = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Designation = table.Column<int>(type: "int", nullable: false),
                    Nationalite = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    NumeroRecuDroit = table.Column<string>(type: "longtext", nullable: true),
                    UrlBordereau = table.Column<string>(type: "longtext", nullable: true),
                    UrlPhotoIdentite = table.Column<string>(type: "longtext", nullable: false),
                    UrlCin = table.Column<string>(type: "longtext", nullable: true),
                    UrlCertificatResidence = table.Column<string>(type: "longtext", nullable: false),
                    UrlCharte = table.Column<string>(type: "longtext", nullable: false),
                    UrlReglement = table.Column<string>(type: "longtext", nullable: false),
                    UrlLivret = table.Column<string>(type: "longtext", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Niveau = table.Column<string>(type: "longtext", nullable: false),
                    AnneeAcademique = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piece_A_Fournir", x => x.IdPiece_A_Fournir);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parcours",
                columns: table => new
                {
                    id_parcours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nom_parcours = table.Column<string>(type: "longtext", nullable: false),
                    code_parcours = table.Column<string>(type: "longtext", nullable: false),
                    id_mention = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcours", x => x.id_parcours);
                    table.ForeignKey(
                        name: "FK_Parcours_Mention_id_mention",
                        column: x => x.id_mention,
                        principalTable: "Mention",
                        principalColumn: "id_mention",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidat",
                columns: table => new
                {
                    IdCandidat = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nom = table.Column<string>(type: "longtext", nullable: false),
                    Prenom = table.Column<string>(type: "longtext", nullable: true),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    LieuNaissance = table.Column<string>(type: "longtext", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nationalite = table.Column<int>(type: "int", nullable: false),
                    SituationMatrimoniale = table.Column<string>(type: "longtext", nullable: false),
                    Adresse = table.Column<string>(type: "longtext", nullable: false),
                    Telephone = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    NumeroCin = table.Column<string>(type: "longtext", nullable: true),
                    DateDelivreCin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LieuDelivreCin = table.Column<string>(type: "longtext", nullable: true),
                    NomPere = table.Column<string>(type: "longtext", nullable: true),
                    NomMere = table.Column<string>(type: "longtext", nullable: false),
                    ProfessionPere = table.Column<string>(type: "longtext", nullable: true),
                    ProfessionMere = table.Column<string>(type: "longtext", nullable: true),
                    AdresseParents = table.Column<string>(type: "longtext", nullable: true),
                    TypeBacc = table.Column<int>(type: "int", nullable: false),
                    SerieBacc = table.Column<int>(type: "int", nullable: true),
                    NumeroBacc = table.Column<string>(type: "longtext", nullable: false),
                    CentreBacc = table.Column<string>(type: "longtext", nullable: false),
                    MentionBacc = table.Column<string>(type: "longtext", nullable: false),
                    MoyenneBacc = table.Column<float>(type: "float", nullable: false),
                    SessionBacc = table.Column<int>(type: "int", nullable: false),
                    id_niveau = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_parcours = table.Column<int>(type: "int", nullable: false),
                    CentreUniversitaire = table.Column<string>(type: "longtext", nullable: false),
                    NumeroRecu = table.Column<string>(type: "longtext", nullable: true),
                    UrlBordereau = table.Column<string>(type: "longtext", nullable: false),
                    UrlPhotoIdentite = table.Column<string>(type: "longtext", nullable: false),
                    UrlActeNaissance = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidat", x => x.IdCandidat);
                    table.ForeignKey(
                        name: "FK_Candidat_Niveau_id_niveau",
                        column: x => x.id_niveau,
                        principalTable: "Niveau",
                        principalColumn: "id_niveau",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidat_Parcours_id_parcours",
                        column: x => x.id_parcours,
                        principalTable: "Parcours",
                        principalColumn: "id_parcours",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Niveau_Parcours",
                columns: table => new
                {
                    id_niveau = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_parcours = table.Column<int>(type: "int", nullable: false),
                    status_selection = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau_Parcours", x => new { x.id_niveau, x.id_parcours });
                    table.ForeignKey(
                        name: "FK_Niveau_Parcours_Niveau_id_niveau",
                        column: x => x.id_niveau,
                        principalTable: "Niveau",
                        principalColumn: "id_niveau",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Niveau_Parcours_Parcours_id_parcours",
                        column: x => x.id_parcours,
                        principalTable: "Parcours",
                        principalColumn: "id_parcours",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Piece_Candidature",
                columns: table => new
                {
                    IdFichier_Candidature = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdCandidat = table.Column<string>(type: "varchar(255)", nullable: false),
                    Designation = table.Column<int>(type: "int", nullable: false),
                    UrlValeur = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piece_Candidature", x => x.IdFichier_Candidature);
                    table.ForeignKey(
                        name: "FK_Piece_Candidature_Candidat_IdCandidat",
                        column: x => x.IdCandidat,
                        principalTable: "Candidat",
                        principalColumn: "IdCandidat",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pre_Selection",
                columns: table => new
                {
                    IdCandidat = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pre_Selection", x => x.IdCandidat);
                    table.ForeignKey(
                        name: "FK_Pre_Selection_Candidat_IdCandidat",
                        column: x => x.IdCandidat,
                        principalTable: "Candidat",
                        principalColumn: "IdCandidat",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pre_Inscription",
                columns: table => new
                {
                    IdPre_Inscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdCandidat = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nom = table.Column<string>(type: "longtext", nullable: false),
                    Prenom = table.Column<string>(type: "longtext", nullable: true),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    UrlDiplomeBacc = table.Column<string>(type: "longtext", nullable: true),
                    UrlReleveBacc = table.Column<string>(type: "longtext", nullable: true),
                    IdPiece_A_Fournir = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pre_Inscription", x => x.IdPre_Inscription);
                    table.ForeignKey(
                        name: "FK_Pre_Inscription_Piece_A_Fournir_IdPiece_A_Fournir",
                        column: x => x.IdPiece_A_Fournir,
                        principalTable: "Piece_A_Fournir",
                        principalColumn: "IdPiece_A_Fournir",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pre_Inscription_Pre_Selection_IdCandidat",
                        column: x => x.IdCandidat,
                        principalTable: "Pre_Selection",
                        principalColumn: "IdCandidat",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inscription",
                columns: table => new
                {
                    matricule = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdPre_Inscription = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscription", x => x.matricule);
                    table.ForeignKey(
                        name: "FK_Inscription_Pre_Inscription_IdPre_Inscription",
                        column: x => x.IdPre_Inscription,
                        principalTable: "Pre_Inscription",
                        principalColumn: "IdPre_Inscription",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Etudiant",
                columns: table => new
                {
                    matricule = table.Column<string>(type: "varchar(255)", nullable: false),
                    sexe = table.Column<int>(type: "int", nullable: false),
                    UrlPhotoIdentite = table.Column<string>(type: "longtext", nullable: false),
                    adresse = table.Column<string>(type: "longtext", nullable: false),
                    telephone = table.Column<string>(type: "longtext", nullable: false),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    id_niveau = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_parcours = table.Column<int>(type: "int", nullable: false),
                    statut = table.Column<int>(type: "int", nullable: false),
                    resultat = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiant", x => x.matricule);
                    table.ForeignKey(
                        name: "FK_Etudiant_Inscription_matricule",
                        column: x => x.matricule,
                        principalTable: "Inscription",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Etudiant_Niveau_id_niveau",
                        column: x => x.id_niveau,
                        principalTable: "Niveau",
                        principalColumn: "id_niveau",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Etudiant_Parcours_id_parcours",
                        column: x => x.id_parcours,
                        principalTable: "Parcours",
                        principalColumn: "id_parcours",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Re_Inscription",
                columns: table => new
                {
                    IdRe_Inscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    matricule = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdPiece_A_Fournir = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Re_Inscription", x => x.IdRe_Inscription);
                    table.ForeignKey(
                        name: "FK_Re_Inscription_Etudiant_matricule",
                        column: x => x.matricule,
                        principalTable: "Etudiant",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Re_Inscription_Piece_A_Fournir_IdPiece_A_Fournir",
                        column: x => x.IdPiece_A_Fournir,
                        principalTable: "Piece_A_Fournir",
                        principalColumn: "IdPiece_A_Fournir",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Candidat_id_niveau",
                table: "Candidat",
                column: "id_niveau");

            migrationBuilder.CreateIndex(
                name: "IX_Candidat_id_parcours",
                table: "Candidat",
                column: "id_parcours");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_id_niveau",
                table: "Etudiant",
                column: "id_niveau");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_id_parcours",
                table: "Etudiant",
                column: "id_parcours");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_IdPre_Inscription",
                table: "Inscription",
                column: "IdPre_Inscription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Niveau_Parcours_id_parcours",
                table: "Niveau_Parcours",
                column: "id_parcours");

            migrationBuilder.CreateIndex(
                name: "IX_Parcours_id_mention",
                table: "Parcours",
                column: "id_mention");

            migrationBuilder.CreateIndex(
                name: "IX_Piece_Candidature_IdCandidat",
                table: "Piece_Candidature",
                column: "IdCandidat");

            migrationBuilder.CreateIndex(
                name: "IX_Pre_Inscription_IdCandidat",
                table: "Pre_Inscription",
                column: "IdCandidat",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pre_Inscription_IdPiece_A_Fournir",
                table: "Pre_Inscription",
                column: "IdPiece_A_Fournir",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Re_Inscription_IdPiece_A_Fournir",
                table: "Re_Inscription",
                column: "IdPiece_A_Fournir",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Re_Inscription_matricule",
                table: "Re_Inscription",
                column: "matricule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Niveau_Parcours");

            migrationBuilder.DropTable(
                name: "Piece_Candidature");

            migrationBuilder.DropTable(
                name: "Re_Inscription");

            migrationBuilder.DropTable(
                name: "Etudiant");

            migrationBuilder.DropTable(
                name: "Inscription");

            migrationBuilder.DropTable(
                name: "Pre_Inscription");

            migrationBuilder.DropTable(
                name: "Piece_A_Fournir");

            migrationBuilder.DropTable(
                name: "Pre_Selection");

            migrationBuilder.DropTable(
                name: "Candidat");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropTable(
                name: "Parcours");

            migrationBuilder.DropTable(
                name: "Mention");
        }
    }
}
