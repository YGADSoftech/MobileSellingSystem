using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSellingProject.Model
{
    public class AccountSettingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string StreetAddress { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}