using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public  UserStatus UserStatus { get; set; }
        public string CustomerEmail { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}