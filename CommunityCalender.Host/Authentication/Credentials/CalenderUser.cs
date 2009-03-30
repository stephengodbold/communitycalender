using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CommunityCalender.Host.Authentication.Credentials
{
    public class CalenderUser : MembershipUser
    {
        public bool IsAdmin { get; set; }

        public string GamerTag { get; set; }
    }
}
