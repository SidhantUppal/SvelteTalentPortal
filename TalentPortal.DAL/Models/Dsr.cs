using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentPortal.DAL.Models
{
    public class Dsr:BaseEntity
    {
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
