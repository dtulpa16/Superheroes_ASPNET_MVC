using System.ComponentModel.DataAnnotations;

namespace SuperheroesApp.Models
{
    public class Superhero
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string AlterEgo { get; set; }

        public string PrimaryAbility { get; set; }

        public string SecondaryAbility { get; set;}

        public string Catchphrase { get; set; }
    }
}
