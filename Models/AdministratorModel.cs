using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Models
{
    public class AdministratorModel
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string ProfilePicture { get; set; }
        public byte[] ProfilePictureUpload { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}