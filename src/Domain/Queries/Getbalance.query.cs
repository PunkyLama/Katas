using Domain.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public class Getbalance : IRequest<float>
    {
        public int Id { get; set; }
    }
}
