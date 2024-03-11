namespace TalentPortal.BAL.Dto
{
    public class DsrDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateTime { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string? Status { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int ProjectId { get; set; }
    }
    public class DsrSearch : DsrDto
    {
        public string ProjectName { get; set; }
        public int DsrLogId { get; set; }
        public List<string> SenderEmails { get; set; }
        public List<string> SenderCCEmails { get; set; }

    }
    public class DsrSubmitDto
    {
        public List<int> DsrIds { get; set; }
        public List<int> SenderIds { get; set; }
        public List<int> SenderCCIds { get; set; }
    }
}
