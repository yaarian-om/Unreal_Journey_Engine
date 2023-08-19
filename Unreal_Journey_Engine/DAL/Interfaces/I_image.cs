using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface I_image<CLASS, PARAM_1,PARAM_2, RET>
    {
        RET Upload_Image(PARAM_1 image, PARAM_2 imageName);
        PARAM_1 Get_Image(PARAM_2 imageName);
    }
}
