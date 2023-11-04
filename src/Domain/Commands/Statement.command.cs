using Domain.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class StatementCommand: IRequest<ICollection<Statement>>
    {
        public int Id { get; set; }
        public int Element { get; set; }
    }
}
