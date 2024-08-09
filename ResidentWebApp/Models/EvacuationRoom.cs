    using System.ComponentModel.DataAnnotations.Schema;

    namespace ResidentWebApp.Models
    {
        [Table("Room", Schema = "dbo")]
        public class EvacuationRoom
        {
            
            [Column("CenterID")]
            public int CenterID { get; set; }

            [Column("RoomID")]
            public int RoomID { get; set; }

            [Column("RoomNumber")]
            public string? RoomNumber { get; set; }

            [Column("Capacity")]
            public int Capacity { get; set; }
             public EvacuationCenter Center { get; set; } = new EvacuationCenter(); 
        }
        

    }
