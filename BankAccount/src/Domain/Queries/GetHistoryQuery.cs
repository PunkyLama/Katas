namespace Domain.Queries
{
    public class GetHistoryQuery : IRequest<ICollection<TransactionHistory>>
    {
        public int Id { get; set; }

        public GetHistoryQuery(int id) 
        {
            Id = id;
        }
    }
}
