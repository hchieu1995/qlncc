using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.HoaDonTongHop
{
    public class FileMauException : Exception
    {
        public FileMauException(string message)
        : base(message)
        {
        }

        public FileMauException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
