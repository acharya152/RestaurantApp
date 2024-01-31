using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Comments
    {
        [Key]
        public int CmtId { get; set; }

        public string? UserId {  get; set; }
        public int? RestroId {  get; set; }
        public string Content {  get; set; }
        public string? UserName {  get; set; }

        
    }

}
