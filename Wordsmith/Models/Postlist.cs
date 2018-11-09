using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordsmith.Models
{
    public   class Postlist
    {
        metodlar klas = new metodlar();
        public   List<tPosts> tPosts()   {   return klas.ConvertToList<tPosts>(klas.GetDataTable("select * from tPosts"));  }
    }
}