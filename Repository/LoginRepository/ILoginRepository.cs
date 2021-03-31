using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEngWebEmployee.Repository
{
    interface ILoginRepository
    {
        Task<bool> IsLoginSuccessfull(AdministratorModel adminModel);
    }
}
