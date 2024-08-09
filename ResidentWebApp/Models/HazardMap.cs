using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("HazardMaps", Schema = "dbo")]
    public class HazardMap
    {
        
        [Column("MapID")]
        public int MapID { get; set; }

        [Column("MapName")]
        public string? MapName { get; set; }

        [Column("ImagePath")]
        public string? ImagePath { get; set; }


    }
    
}
