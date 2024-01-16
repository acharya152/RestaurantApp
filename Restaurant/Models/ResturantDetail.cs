using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class ResturantDetail
    {
        [Key]
        public string ID { get; set; }  
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; } 

    }
}
