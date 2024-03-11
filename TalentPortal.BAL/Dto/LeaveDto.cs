namespace TalentPortal.BAL.Dto
{
    public class LeaveDto
    {
        public int Id { get; set; }
        public int LeaveTypeId { get; set; }
        public int SNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }
        public string[] SenderEmailIds { get; set; }
        public string[] SenderCCEmailIds { get; set; }
        public int UserId { get; set; }

    }
    public class LeaveSearch : LeaveDto
    {
        public string Shift { get; set; }
        public string Status { get; set; }
    }
}
