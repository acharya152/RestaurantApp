using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class DetailsRestro
    {
        [Key] public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string DetailedDescription { get; set; }    
        public string PhoneNo { get; set; }
        public TimeOnly Time { get; set; }
        public TimeOnly CloseTime { get; set; } 
        public string Website { get; set; }

       public string? Photo { get; set; }
       
       


    }
}
