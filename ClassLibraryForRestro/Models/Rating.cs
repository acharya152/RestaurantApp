using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Rating
    {
        [Key] public int RateID { get; set; }
        public int? RestroID { get; set; }
        public int? Ratings { get; set; }
    }
}
