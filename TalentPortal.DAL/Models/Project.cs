using System.ComponentModel.DataAnnotations.Schema;

namespace TalentPortal.DAL.Models
{
    [Table("Projects")]
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
