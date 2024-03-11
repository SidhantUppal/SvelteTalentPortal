namespace TalentPortal.DAL.Models
{
    public class AttendanceLog : BaseEntity
    {
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public TimeSpan? TotalWorkingHours { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
