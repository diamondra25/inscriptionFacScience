using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace back.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public required string nom { get; set; }
        public string? prenom { get; set; }
        public required string role { get; set; }
        public  string password {get; private set; }


        public void SetPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                this.password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Mettez à jour l'attribut password
            }
        }

        public bool ValidatePassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string inputPasswordHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                
                return this.password == inputPasswordHash; 
            }
        }
    }
}
