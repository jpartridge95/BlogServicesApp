using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(int id) : base(
            String.Format("No record found for id: {0}", id))
        {
        }
    }
}
