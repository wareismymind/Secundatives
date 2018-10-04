using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wimm.Secundatives.Exceptions
{
    public class ExpectException : Exception
    {

        public ExpectException(string message) : base(message)
        {
        }
    }
}
