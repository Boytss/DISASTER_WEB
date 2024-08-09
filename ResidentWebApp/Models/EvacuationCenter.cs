using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("EvacuationCenter", Schema = "dbo")]
    public class EvacuationCenter
    {
        
        [Column("CenterID")]
        public int CenterID { get; set; }

        [Column("CenterName")]
        public string? CenterName { get; set; }

        [Column("Location")]
        public string? Location { get; set; }
        
        [Column("Date")]
        public DateTime Date { get; set; }
        public ICollection<EvacuationRoom>? Rooms { get; set; }
    }
    
}
