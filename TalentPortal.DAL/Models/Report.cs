using System.ComponentModel.DataAnnotations.Schema;

namespace TalentPortal.DAL.Models
{
    [Table("Reports")]
    public class Report : BaseEntity
    {
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
