using System.ComponentModel.DataAnnotations;

namespace Bibiki.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsActive { get; set; }

    }
}
