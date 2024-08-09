using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("Disasters", Schema = "dbo")]
    public class Disaster
    {
         [Column("DisasterID")]
        public int DisasterID { get; set; }
         [Column("DisasterName")]
        public string? DisasterName { get; set; }
         [Column("PictureLogoPath")]
        public string? PictureLogoPath { get; set; }
    }
}