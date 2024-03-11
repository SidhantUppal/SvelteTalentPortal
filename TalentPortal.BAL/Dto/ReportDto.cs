namespace TalentPortal.BAL.Dto
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public List<string> SenderEmailIds { get; set; }
        public List<string> SenderCCEmailIds { get; set; }
        public int UserId { get; set; }
    }
    public class ReportSearch : ReportDto
    {
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
    }
}
