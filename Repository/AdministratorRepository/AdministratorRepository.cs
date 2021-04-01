using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Repository.AdministratorRepository
{
    public class AdministratorRepository : IAdministratorRepository
    {
        public IEnumerable<AdministratorModel> FetchAdministrators()
        {
            List<AdministratorModel> mockList = new List<AdministratorModel>();
            mockList.Add(new AdministratorModel {
                Username = "Sample Username 1",
                User_ID = 101010101,
                User_Password = "Sample Password 1",
                User_Image = "Sample IMage"
            });
            mockList.Add(new AdministratorModel
            {
                Username = "Sample Username 2",
                User_ID = 101010103,
                User_Password = "Sample Password 2",
                User_Image = "Sample IMage"
            });
            mockList.Add(new AdministratorModel
            {
                Username = "Sample Username 2",
                User_ID = 101010102,
                User_Password = "Sample Password 2",
                User_Image = "Sample IMage"
            });
            return mockList;
        }
    }
}