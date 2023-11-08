namespace Web.API.Models.Responses
{
    public class StatementReponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OperationString { get; set; }
        public string StatementStatusString { get; set; }
        public float Amount { get; set; }
        public float OldBalance { get; set; }
        public float? NewBalance { get; set; }
    }
}
