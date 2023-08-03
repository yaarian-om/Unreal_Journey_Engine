using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    internal interface IRepo<CLASS, ID, RET>
    {

        List<CLASS> Get();
        CLASS Get(ID id);
        RET Create(CLASS obj);
        bool Delete(ID id);
        RET Update(CLASS obj);

    }
}
