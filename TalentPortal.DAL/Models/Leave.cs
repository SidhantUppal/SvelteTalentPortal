namespace TalentPortal.DAL.Models
{
    public class Leave : BaseEntity
    {
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }
        public string SenderEmailIds { get; set; }
        public string SenderCCEmailIds { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
