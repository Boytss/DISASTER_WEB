using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentWebApp.Models
{
    [Table("TutorialTexts", Schema = "dbo")]
    public class Tip
    {
        
         [Column("TextID")]
        public int TextID { get; set; }
        [Column("DisasterID")]
        public int DisasterID { get; set; }
        [Column("TutorialText")]
        public string? TutorialText { get; set; }
    }
}