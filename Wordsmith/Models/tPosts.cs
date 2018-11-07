using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordsmith.Models
{
    public class tPosts
    {
        public int IDPost { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public string PostImage { get; set; }
        public int IDUser { get; set; }
        public virtual tCategorys tCategorys { get; set; }
    }
}