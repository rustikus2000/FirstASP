using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibiki.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        public virtual Brand? Brand { get; set; }

    }
}
