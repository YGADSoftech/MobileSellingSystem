using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSellingProject.Model
{
    public class Login
    {
        public string LoginId { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}