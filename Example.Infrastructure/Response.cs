using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastructure
{
    public class Response<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}