using System.ComponentModel.DataAnnotations;

namespace Kantar.Randstad.GPS.Entities
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}