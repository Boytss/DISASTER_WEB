using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("NewsEvents", Schema = "dbo")]
    public class NewsEvents
    {
        
        [Column("EventID")]
        public int EventID { get; set; }

        [Column("Title")]
        public string? Title { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("ImagePath")]
        public string? ImagePath { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

       [Column("By")]
        public string? By { get; set; }
    }
    
}
