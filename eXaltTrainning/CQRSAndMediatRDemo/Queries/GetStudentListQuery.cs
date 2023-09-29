using CQRSAndMediatRDemo.Models;
using MediatR;

namespace CQRSAndMediatRDemo.Queries
{
    //Read Query
    public class GetStudentListQuery : IRequest<List<StudentDetails>>
    {
    }
}
