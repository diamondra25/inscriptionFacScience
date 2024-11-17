namespace back.Models.Dto
{
    public class AdminDto
    {
        public required string Nom { get; set; }
        public string? Prenom { get; set; }
        public required string Role { get; set; }
        public required string Password { get; set; }
    }
}
