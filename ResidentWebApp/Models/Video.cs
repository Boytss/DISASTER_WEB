using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("EducationalVideos", Schema = "dbo")]
    public class Video
    {
        
        [Column("VideoID")]
        public int VideoID { get; set; }

        [Column("Title")]
        public string? Title { get; set; }

        [Column("VideoURL")]
        public string? VideoURL { get; set; }
    }
    
}
