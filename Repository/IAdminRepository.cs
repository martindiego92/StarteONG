using starteAlkemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starteAlkemy.Repository.IRepository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin Get( string password, string email);

    }
}