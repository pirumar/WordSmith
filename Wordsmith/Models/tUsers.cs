using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordsmith.Models
{
    public class tUsers
    {
        ///IDUser, UserName, UsernSname, UserMail, UserPass, UserImage, IDRole
        ///
        public int IDUser { get; set; }
        public string UserName { get; set; }
        public string UsernSname { get; set; }
        public string UserMail { get; set; }
        public string UserPass { get; set; }
        public string UserImage { get; set; }
        public int IDRole { get; set; }
    }
}