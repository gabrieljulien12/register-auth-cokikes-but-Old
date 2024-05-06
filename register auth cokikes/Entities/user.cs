using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace register_auth_cokikes.Entities
{
    [Table("AuthUser")]
    public class user
    {
        [Key]
        public Guid Id { get; set; }


        [StringLength(50)]
         public string? Fullname { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public  string Password { get; set; }
        public bool Locked { get; set; }=false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string Role { get; set; } = "user";

    }
}
