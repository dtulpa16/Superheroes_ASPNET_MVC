using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperheroesApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public int SuperheroId { get; set; }

        [ForeignKey("SuperheroId")]
        public Superhero Superhero { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }
    }
}
