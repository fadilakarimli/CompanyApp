using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string messages) : base(messages) { }
        
    }
}
