using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSSystem.Application.Contracts.CommonDtos
{
    public class ListDto<T>
    {
        public int Count { get; set; }

        public List<T> List { get; set; }
    }
}