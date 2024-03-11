namespace TalentPortal.Helpers
{
    public class ResponseModel<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
    }

}
