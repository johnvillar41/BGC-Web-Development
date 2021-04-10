using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class SalesSpecificRepository
    {
        private static SalesSpecificRepository instance = null;
        private SalesSpecificRepository()
        {

        }
        public static SalesSpecificRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new SalesSpecificRepository();
            }
            return instance;
        }
        public List<SalesSpecificRepository> FetchSpecificSale()
        {
            throw new NotImplementedException();
        }
    }
}