using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordsmith.Models
{
    public class tCategorys
    {
        public int IDCategory { get; set; }
        public string CategoryName { get; set; }
        public int IDParentCategory { get; set; }
        public List<tCategorys> Children { get; set; }

    }
}