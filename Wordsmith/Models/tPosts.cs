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
        public int IDCategory { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string Tags { get; set; }
        public List<tCategorys> TCategorys { get; set; }
        public List<tTags> tTags { get; set; }
        public tUsers tUsers { get; set; }
        public int  Slider { get; set; }
        public int PostRead { get; set; }
        public DateTime PostDate { get; set; }
    }
}