namespace back.Models.Services
{
    public class MentionService
    {
        private readonly DatabaseContext _context;
        public MentionService(DatabaseContext context)
        {
            _context = context;
        }

        public List<MentionDto> GetMentionsByNiveau(string niveau)
        {
            var result = (from m in _context.Mention
                         join p in _context.Parcours on m.id_mention equals p.id_mention
                         join np in _context.Niveau_Parcours on p.id_parcours equals np.id_parcours
                         where np.id_niveau == niveau
                         select new MentionDto
                         {
                             IdMention = m.id_mention,
                             NomMention = m.nom_mention
                         }).Distinct();

            return result.ToList();
        }
    }

    // DTO (Data Transfer Object) for Mention
    public class MentionDto
    {
        public int IdMention { get; set; }
        public required string NomMention { get; set; }
    }

}

