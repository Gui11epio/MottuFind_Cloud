using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MottuFind_C_.Domain.Entities
{
    public class Resource<T>
    {
        public T Data { get; set; }
        public List<Link> Links { get; set; } = new();
    }
}
