using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurSpotKC_Blazor_Server.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Resource> Resources { get; set; }
    }
}
