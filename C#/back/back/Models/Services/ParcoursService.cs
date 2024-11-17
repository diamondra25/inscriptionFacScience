namespace back.Models.Services
{
    public class ParcoursService
    {
        private readonly DatabaseContext _context;
        public ParcoursService(DatabaseContext context)
        {
            _context = context;
        }


        public List<ParcoursDto> GetParcoursByMention(int IdMention)
        {
            var result = from p in _context.Parcours
                         join m in _context.Mention on p.id_mention equals m.id_mention
                         where m.id_mention == IdMention
                         select new ParcoursDto
                         {
                             id_parcours = p.id_parcours,
                             nom_parcours = p.nom_parcours
                         };

            return result.ToList();
        }
    }

    public class ParcoursDto
    {
        public int id_parcours { get; set; }
        public required string nom_parcours { get; set; }
    }

}

