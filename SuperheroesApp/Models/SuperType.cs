using System.ComponentModel.DataAnnotations;

namespace SuperheroesApp.Models
{
    public class SuperType
    {
        [Key] 
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
