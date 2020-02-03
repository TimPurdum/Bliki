using System.ComponentModel.DataAnnotations;


namespace Bliki.Data
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}